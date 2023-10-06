using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Handlers.Interfaces
{
    public interface IAuthHandler
    {
        Task<User> Register(User entity, string password);
        Task<User> Update(User entity, string password);
        Task<TokenDto> Login(UserLoginDto dto, bool isEmail);
        Task<GenericCommandResult<object>> RegisterRolesToUser(List<UserRole> entity);
        Task<bool> Logout();
    }
}
