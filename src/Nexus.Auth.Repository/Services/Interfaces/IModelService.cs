using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Model;
using Nexus.Auth.Repository.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IModelService : IGenericService<ModelDto, ModelResponseDto>
{
    Task<GenericCommandResult<IEnumerable<ModelResponseDto>>> GetByManufacturerId(GetById dto, string path);
    Task<GenericCommandResult<ModelFromVdsResponseDto>> GetByVds(GetByVdsDto obj, string path);
}