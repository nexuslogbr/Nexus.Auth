using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class DamageTypeService : IDamageTypeService
{
    private readonly IAccessDataService _accessDataService;

    public DamageTypeService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<DamageTypeResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<DamageTypeResponseDto>>(path, "api/v1/DamageType/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<DamageTypeResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<DamageTypeResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DamageTypeResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DamageTypeResponseDto>(path, "api/v1/DamageType/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<DamageTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DamageTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DamageTypeResponseDto>> Post(DamageTypeDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DamageTypeResponseDto>(path, "api/v1/DamageType/Post", obj);
        if (result is not null)
            return new GenericCommandResult<DamageTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DamageTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DamageTypeResponseDto>> Put(DamageTypeDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DamageTypeResponseDto>(path, "api/v1/DamageType/Put", obj);
        if (result is not null)
            return new GenericCommandResult<DamageTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DamageTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/DamageType/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}