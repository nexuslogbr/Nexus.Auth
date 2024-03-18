using Nexus.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IUserService<T> : IBaseService<T> where T : class
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
        Task<IList<string>> GetRolesByIdAsync(User user);
        Task<IdentityResult> RegisterRoles(User entity);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> RemoveRoles(User user, IList<string> userRoles);
        Task<IList<Role>> AddRoles(List<UserRole> list);
        Task<bool> DeleteRoles(int userId);
        Task<bool> ChangeStatus(User entity, bool status);
        Task<List<UserPlace>> GetPlacesByUserId(int userId);
    }
}
