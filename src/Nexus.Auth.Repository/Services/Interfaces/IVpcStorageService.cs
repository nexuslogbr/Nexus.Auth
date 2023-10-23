using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.VpcStorage;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IVpcStorageService : IGenericService<VpcStorageDto, VpcStorageResponseDto>
{
    Task<GenericCommandResult<PageList<VpcStorageFlatResponseDto>>> GetAllFlat(PageParams pageParams, string path);
}