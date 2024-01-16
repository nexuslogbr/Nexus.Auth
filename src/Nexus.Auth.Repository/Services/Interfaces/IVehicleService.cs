using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.VehicleInfo;
using Nexus.Auth.Repository.Dtos.Vehicle;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IVehicleService
{
    Task<GenericCommandResult<PageList<VehicleResponseDto>>> GetAll(PageParams pageParams, string path);
    Task<GenericCommandResult<object>> PostRange(List<VehicleDto> entities, string path);
    Task<GenericCommandResult<TokenDto>> Delete(GetById dto, string path);
    Task<GenericCommandResult<VehicleResponseDto>> GetById(GetById dto, string path);
    Task<GenericCommandResult<VehicleResponseDto>> GetByName(GetByName dto, string path);
}