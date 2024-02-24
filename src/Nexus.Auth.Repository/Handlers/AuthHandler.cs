﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Place;
using Nexus.Auth.Repository.Dtos.Role;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nexus.Auth.Repository.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IAuthService _authService;
        private readonly IUserService<User> _userService;
        private readonly IRoleService<Role> _roleService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IPlaceService _placeService;

        public AuthHandler(
            IAuthService authService,
            IUserService<User> userService,
            IConfiguration config,
            IRoleService<Role> roleService,
            IMapper mapper,
            IPlaceService placeService)
        {
            _authService = authService;
            _userService = userService;
            _config = config;
            _roleService = roleService;
            _mapper = mapper;
            _placeService = placeService;
        }

        public async Task<UserModel> Register(UserDto entity)
        {
            PlaceResponseDto place = null;

            if (entity.Mobile)
            {
                place = (await _placeService.GetById(entity.PlaceId)).Data;
                if (place is null) throw new Exception("Local inválido");
            }

            var user = _mapper.Map<User>(entity);
            user.PlaceId = place is not null ? place.Id : 0;
            user.PlaceName = place is not null ? place.Name + " - "+ place.Acronym : string.Empty;
            user.RegisterDate = DateTime.Now;
            user.Roles = _mapper.Map<List<Role>>(entity.Roles);
            var result = await _authService.Register(user, entity.Password);

            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Update(UserIdDto entity)
        {
            PlaceResponseDto place = null;
            if (entity.Mobile)
            {
                place = (await _placeService.GetById(entity.PlaceId)).Data;
                if (place is null || place.Id == 0) throw new Exception("Local inválido");
            }

            var user = await _userService.GetByIdAsync(entity.Id);
            var updated = _mapper.Map(entity, user);
            if (user is null)
                throw new Exception("Usuário não encontrado.");

            user.PlaceId = place is not null ? place.Id : 0;
            user.PlaceName = place is not null ? place.Name + " - " + place.Acronym : string.Empty;
            user.ChangeDate = DateTime.Now;
            user.Roles = updated.Roles;

            await _userService.DeleteRoles(user.Id);
            var result = !string.IsNullOrEmpty(entity.Password) ? await UpdateUserWithPass(user, entity.Password) :  await _authService.UpdateUserNoPass(user);

            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> UpdatePassword(UserIdDto entity)
        {
            var user = await _userService.GetByEmailAsync(entity.Email);
            if (user is null)
                throw new Exception("Usuário não encontrado.");

            user.ChangeDate = DateTime.Now;

            var result = !string.IsNullOrEmpty(entity.Password) ?
                await UpdateUserWithPass(user, entity.Password) :
                await _authService.UpdateUserNoPass(user);
            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<GenericCommandResult<object>> RegisterRolesToUser(List<UserRole> entity)
        {
            var rolesOfUser = new List<UserRole>();

            foreach (var item in entity)
            {
                var user = await _authService.FindUserByUserId(item.UserId);
                if (user is null)
                    return new GenericCommandResult<object>(false, "Non-existent user", new object { }, StatusCodes.Status404NotFound);

                var role = await _authService.FindRoleByRoleId(item.RoleId);
                if (role is null)
                    return new GenericCommandResult<object>(false, "Non-existent role", new object { }, StatusCodes.Status404NotFound);

                user.UserRoles = new List<UserRole> { new UserRole { User = user, Role = role } };
                var result = await _authService.RegisterRoles(user);

                if (result.Succeeded)
                    rolesOfUser.Add(new UserRole { Role = role, User = user });
                else
                    return new GenericCommandResult<object>(false, result.Errors.ToList()[0].Description, new object { }, StatusCodes.Status400BadRequest);

            }

            return new GenericCommandResult<object>(true, "Successful creation", true, StatusCodes.Status200OK);
        }

        public async Task<AuthResult> Login(UserLoginDto dto, bool isEmail, bool mobile)
        {
            User user = null;
            if (isEmail)
                user = await _authService.FindUserByEmail(dto.UserName);
            else if (!string.IsNullOrEmpty(dto.UserName))
                user = await _authService.FindUserByUserName(dto.UserName);

            if (user is null)
                return null;
            else if (mobile && !user.Mobile)
                return null;
            else
            {
                user.ResetPasswordToken = await _userService.GeneratePasswordResetTokenAsync(user);
                var result = await _authService.CheckPasswordSignIn(user, dto.Password);
                if (result.Succeeded)
                    return new AuthResult
                    {
                        Token = await GenerateJWToken(user),
                        User = _mapper.Map<UserResult>(user),
                        Roles = _mapper.Map<RoleResult[]>(user.Roles)
                    };
            }

            return null;
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Locality, user.PlaceName)
            };

            var roles = await _roleService.GetByUserIdAsync(user.Id);
            foreach (var role in roles)
            {
                var menus = role.RoleMenus.Select(x => x.Menu).Select(x => x.Name);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                claims.Add(new Claim("menus", string.Join(",", menus)));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Logout()
        {
            var success = await _authService.Logout();

            if (success)
                return true;

            return false;
        }

        protected async Task<IdentityResult> UpdateUserWithPass(User user, string password)
        {
            return await _authService.UpdateUserWithPass(user, password);
        }
    }
}
