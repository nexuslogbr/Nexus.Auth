using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Dtos.VehicleInfo;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class VehicleInfoService : IVehicleInfoService
{
    private readonly IAccessDataService _accessDataService;

    public VehicleInfoService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<VehicleInfoResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<VehicleInfoResponseDto>>(path, "api/v1/VehicleInfo/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<VehicleInfoResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<VehicleInfoResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileRegisterResultDto>> PostRange(IEnumerable<string> entities, string path)
    {
        var result = await _accessDataService.PostDataAsync<UploadFileRegisterResultDto>(path, "api/v1/VehicleInfo/PostRange", entities);
        if (result is not null)
            return new GenericCommandResult<UploadFileRegisterResultDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileRegisterResultDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/VehicleInfo/Delete", dto);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VehicleInfoResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<VehicleInfoResponseDto>(path, "api/v1/VehicleInfo/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<VehicleInfoResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VehicleInfoResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VehicleInfoResponseDto>> GetByName(GetByName dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<VehicleInfoResponseDto>(path, "api/v1/VehicleInfo/GetByName", dto);
        if (result is not null)
            return new GenericCommandResult<VehicleInfoResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VehicleInfoResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}