using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Dtos.Role;

namespace Nexus.Auth.Repository.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IAuthService _authService;
        private readonly IUserService<User> _userService;
        private readonly IRoleService<Role> _roleService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthHandler(
            IAuthService authService,
            IUserService<User> userService,
            IConfiguration config,
            IRoleService<Role> roleService,
            IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _config = config;
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<UserModel> Register(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            user.ChangeDate = DateTime.Now;
            user.RegisterDate = DateTime.Now;
            var result = await _authService.Register(user, entity.Password);

            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Update(UserIdDto entity)
        {
            var user = await _userService.GetByIdAsync(entity.Id);
            if (user is null)
                throw new Exception("Usuário não encontrado.");

            await _userService.DeleteRoles(user.Id);
            var updated = _mapper.Map(entity, user);
            updated.ChangeDate = DateTime.Now;

            var result = !string.IsNullOrEmpty(entity.Password) ?
                await _authService.UpdateUserWithPass(updated, entity.Password) :
                await _authService.UpdateUserNoPass(updated);
            if (!result.Succeeded)
                throw new Exception("Erro ao salvar usuário.");

            return _mapper.Map<UserModel>(updated);
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

        public async Task<AuthResult> Login(UserLoginDto dto, bool isEmail)
        {
            User user = null;
            if (isEmail)
                user = await _authService.FindUserByEmail(dto.UserName);
            else if (!string.IsNullOrEmpty(dto.UserName))
                user = await _authService.FindUserByUserName(dto.UserName);

            if (user is null)
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
                new Claim(ClaimTypes.Email, user.Email)
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

    }
}
