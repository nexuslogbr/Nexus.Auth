using Nexus.Auth.Repository.Dtos.Chassis;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.VehicleInfo;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IChassisService
{
    Task<GenericCommandResult<ChassisResponseDto>> GetById(GetById dto, string path);
    Task<GenericCommandResult<IEnumerable<ChassisInfoResponseDto>>> GetChassisInfo(ChassisInfoParams pageParams, string path);
    Task<GenericCommandResult<PageList<ChassisResponseDto>>> GetAll(PageParams pageParams, string path);
}