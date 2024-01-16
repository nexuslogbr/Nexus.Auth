using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.Vehicle;
using Nexus.Auth.Repository.Dtos.VehicleInfo;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class VehicleService : IVehicleService
{
    private readonly IAccessDataService _accessDataService;

    public VehicleService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<VehicleResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<VehicleResponseDto>>(path, "api/v1/Vehicle/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<VehicleResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<VehicleResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<object>> PostRange(List<VehicleDto> entities, string path)
    {
        var result = await _accessDataService.PostDataAsync<object>(path, "api/v1/Vehicle/PostRange", entities);
        if (result is not null)
            return new GenericCommandResult<object>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<object>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Vehicle/Delete", dto);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VehicleResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<VehicleResponseDto>(path, "api/v1/Vehicle/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<VehicleResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VehicleResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VehicleResponseDto>> GetByName(GetByName dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<VehicleResponseDto>(path, "api/v1/Vehicle/GetByName", dto);
        if (result is not null)
            return new GenericCommandResult<VehicleResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VehicleResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}