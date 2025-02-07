using Nexus.Auth.Domain.Entities;

namespace Nexus.Auth.Repository.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJWToken(User user);
    }
}
