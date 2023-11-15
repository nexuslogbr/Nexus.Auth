using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Nexus.Auth.Repository.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService() { }

        public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> FindUser(string username) => await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == username.ToUpper());

        public async Task<User> FindUserByUserId(int id) => await _userManager.FindByIdAsync(id.ToString());

        public async Task<Role> FindRoleByRoleId(int id) => await _roleManager.FindByIdAsync(id.ToString());

        public async Task<User> FindUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) { return null; }

            var roles = await _userManager.GetRolesAsync(user);
            user.Roles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            return user;
        } 
        
        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { return null; }

            var roles = await _userManager.GetRolesAsync(user);
            user.Roles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            return user;

        }

        public async Task<IdentityResult> Register(User model, string password) 
        {
            var result = await _userManager.CreateAsync(model, password);
            
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(model);

            return result;
        }
            
        public async Task<IdentityResult> UpdateUserWithPass(User user, string password) 
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                result = await _userManager.ResetPasswordAsync(user, token, password);
            }
            return result;
        }

        public async Task<IdentityResult> UpdateUserNoPass(User model)
        {
            return await _userManager.UpdateAsync(model);
        }

        public async Task<SignInResult> CheckPasswordSignIn(User user, string password) => await _signInManager.CheckPasswordSignInAsync(user, password, false);

        public async Task<IList<string>> GetRoles(User model)
        {
            return await _userManager.GetRolesAsync(model);
        }

        public async Task<IdentityResult> RegisterRoles(User entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<bool> Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("CookieAuthScheme");
            return true;
        }

    }
}
