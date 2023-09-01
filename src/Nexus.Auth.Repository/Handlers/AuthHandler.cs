using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Handlers.Interfaces;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private readonly IAuthService _authService;
        private readonly IUserService<User> _userService;
        private readonly IConfiguration _config;

        public AuthHandler(IAuthService authService, IUserService<User> userService, IConfiguration config)
        {
            _authService = authService;
            _userService = userService;
            _config = config;
        }

        public async Task<User> Register(User entity, string password)
        {

            if ((await _authService.Register(entity, password)).Succeeded)
            {
                var user = await _userService.GetByNameAsync(entity.Name);
                user.Roles = await _userService.AddRoles(
                    entity.Roles.Select(role =>
                                                new UserRole
                                                {
                                                    RoleId = role.Id,
                                                    UserId = user.Id
                                                }).ToList()
                );

                return user;
            }

            throw new Exception("Error saving of entity");
        }

        public async Task<User> Update(User entity, string password)
        {
            var user = await _userService.GetByIdAsync(entity.Id);
            user.Name = string.IsNullOrEmpty(entity.Name) ? user.Name : entity.Name;
            user.Email = string.IsNullOrEmpty(entity.Email) ? user.Email : entity.Email;
            user.UserName = string.IsNullOrEmpty(entity.UserName) ? user.UserName : entity.UserName;
            user.ChangeDate = DateTime.Now;

            var result = !string.IsNullOrEmpty(password) ? (await _authService.UpdateUserWithPass(user, password)).Succeeded : (await _authService.UpdateUserNoPass(user)).Succeeded;

            if (result)
            {
                if (await _userService.DeleteRoles(user.Id))
                {
                    var users = entity.Roles.Select(role => new UserRole { RoleId = role.Id, UserId = user.Id }).ToList();
                    user.Roles = await _userService.AddRoles(users);
                }

                return user;
            }

            throw new Exception("Error update of entity");
        }

        public async Task<GenericCommandResult> RegisterRolesToUser(List<UserRole> entity)
        {
            var rolesOfUser = new List<UserRole>();

            foreach (var item in entity)
            {
                var user = await _authService.FindUserByUserId(item.UserId);
                if (user is null)
                    return new GenericCommandResult(false, "Non-existent user", new object { }, StatusCodes.Status404NotFound);

                var role = await _authService.FindRoleByRoleId(item.RoleId);
                if (role is null)
                    return new GenericCommandResult(false, "Non-existent role", new object { }, StatusCodes.Status404NotFound);

                user.UserRoles = new List<UserRole> { new UserRole { User = user, Role = role } };
                var result = await _authService.RegisterRoles(user);

                if (result.Succeeded)
                    rolesOfUser.Add(new UserRole { Role = role, User = user });
                else
                    return new GenericCommandResult(false, result.Errors.ToList()[0].Description, new object { }, StatusCodes.Status400BadRequest);

            }

            return new GenericCommandResult(true, "Successful creation", true, StatusCodes.Status200OK);
        }

        public async Task<TokenDto> Login(UserLoginDto dto, bool isEmail)
        {
            User user = null;
            if (isEmail)
                user = await _authService.FindUserByEmail(dto.UserName);
            else if (!string.IsNullOrEmpty(dto.UserName))
                user = await _authService.FindUserByUserName(dto.UserName);
            else
                return null;

            var result = await _authService.CheckPasswordSignIn(user, dto.Password);

            if (result.Succeeded)
                return new TokenDto { Token = await GenerateJWToken(user) };

            return null;
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _authService.GetRoles(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
