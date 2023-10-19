using Azure.Core;
using Azure;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.VehicleInfo;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IVehicleInfoService
{
    Task<GenericCommandResult<PageList<VehicleInfoResponseDto>>> GetAll(PageParams pageParams, string path);
    Task<GenericCommandResult<UploadFileRegisterResultDto>> PostRange(IEnumerable<string> entities, string path);
    Task<GenericCommandResult<TokenDto>> Delete(GetById dto, string path);
    Task<GenericCommandResult<VehicleInfoResponseDto>> GetById(GetById dto, string path);
}