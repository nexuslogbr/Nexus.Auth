using Nexus.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(User user, string password);
        Task<IdentityResult> UpdateUserWithPass(User model, string password);
        Task<IdentityResult> UpdateUserNoPass(User model);
        Task<IdentityResult> RegisterRoles(User entity);
        Task<SignInResult> CheckPasswordSignIn(User user, string password);
        Task<User> FindUserByUserName(string username);
        Task<User> FindUserByUserId(int id);
        Task<Role> FindRoleByRoleId(int id);
        Task<User> FindUserByEmail(string email);
        Task<User> FindUser(string username);
        Task<IList<string>> GetRoles(User model);
        Task<bool> Logout();
    }
}
