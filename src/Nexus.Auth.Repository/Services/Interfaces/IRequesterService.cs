using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Requester;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IRequesterService : IGenericService<RequesterDto, RequesterResponseDto>
{
    Task<GenericCommandResult<RequesterResponseDto>> GetByNameOrCode(GetByNameCodeDto obj, string path);
}