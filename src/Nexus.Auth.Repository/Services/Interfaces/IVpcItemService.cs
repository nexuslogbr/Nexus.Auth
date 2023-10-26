using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.VpcItem;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IVpcItemService : IGenericService<VpcItemDto, VpcItemResponseDto>
{
    Task<GenericCommandResult<IEnumerable<VpcItemResponseDto>>> GetByModelId(GetById dto, string path);
}