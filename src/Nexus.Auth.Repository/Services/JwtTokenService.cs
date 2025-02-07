using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Nexus.Auth.Repository.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly IRoleService<Role> _roleService;

        public JwtTokenService(IConfiguration config, IRoleService<Role> roleService)
        {
            _config = config;
            _roleService = roleService;
        }

        public async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("PlaceId", user.PlaceId.ToString())
            };

            var roles = await _roleService.GetByUserIdAsync(user.Id);
            foreach (var role in roles)
            {
                var menus = role.RoleMenus.Select(x => x.Menu).Select(x => x.Name);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                claims.Add(new Claim("menus", string.Join(",", menus)));
            }

            var keyString = _config.GetSection("AppSettings:Key").Value;
            if (string.IsNullOrEmpty(keyString) || keyString.Length < 64)
            {
                throw new InvalidOperationException("A chave de criptografia deve ter pelo menos 64 caracteres.");
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
