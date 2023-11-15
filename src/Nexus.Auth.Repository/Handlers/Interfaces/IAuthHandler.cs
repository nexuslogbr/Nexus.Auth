using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.User;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IAuthHandler
    {
        Task<UserModel> Register(UserDto entity);
        Task<UserModel> Update(UserIdDto entity);
        Task<AuthResult> Login(UserLoginDto dto, bool isEmail);
        Task<GenericCommandResult<object>> RegisterRolesToUser(List<UserRole> entity);
        Task<bool> Logout();
    }
}
