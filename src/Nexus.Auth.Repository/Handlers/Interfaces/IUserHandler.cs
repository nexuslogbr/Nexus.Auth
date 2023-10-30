using Nexus.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IUserHandler<T> : IBaseHandler<T> where T : class
    {
        Task<PageList<UserModel>> GetAll(PageParams pageParams);
        Task<User> GetById(int id);
        Task<User> GetByName(string name);
        Task<User> GetByEmail(string email);
        Task<IList<string>> GetRolesByUser(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task GeneratePasswordResetTokenAsync(User user);
        Task<bool> AddRoleByUser(User user, Role role);
        Task<bool> UpdateRoleByUser(User user, Role role);
        Task<IdentityResult> RemoveFromRoles(User user, IList<string> userRoles);
    }
}
