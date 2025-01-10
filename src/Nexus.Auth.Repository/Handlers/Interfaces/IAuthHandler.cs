using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IAuthHandler
    {
        Task<UserModel> Register(UserDto entity, int userId);
        Task<UserModel> Update(UserPutDto entity);
        Task<UserModel> UpdatePassword(UserDto entity);
        Task<AuthResult> Login(UserLoginDto dto, bool isEmail, bool mobile);
        Task<GenericCommandResult<object>> RegisterRolesToUser(List<UserRole> entity);
        Task<bool> Logout();
    }
}
