using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.Chassis;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class ChassisService : IChassisService
{
    private readonly IAccessDataService _accessDataService;

    public ChassisService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<ChassisResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChassisResponseDto>(path, "api/v1/Chassis/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<ChassisResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChassisResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<IEnumerable<ChassisInfoResponseDto>>> GetChassisInfo(ChassisInfoParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<ChassisInfoResponseDto>>(path, "api/v1/Chassis/GetChassisInfo", pageParams);
        if (result is not null)
            return new GenericCommandResult<IEnumerable<ChassisInfoResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<IEnumerable<ChassisInfoResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<PageList<ChassisResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<ChassisResponseDto>>(path, "api/v1/Chassis/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<ChassisResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<ChassisResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}