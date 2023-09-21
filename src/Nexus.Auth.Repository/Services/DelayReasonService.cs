using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class DelayReasonService : IDelayReasonService
{

    private readonly IAccessDataService _accessDataService;

    public DelayReasonService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<IEnumerable<DelayReasonResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<DelayReasonResponseDto>>(path, "api/v1/DelayReason/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<IEnumerable<DelayReasonResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<IEnumerable<DelayReasonResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DelayReasonResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DelayReasonResponseDto>(path, "api/v1/DelayReason/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<DelayReasonResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DelayReasonResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<IEnumerable<DelayReasonResponseDto>>> GetByName(GetByName obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<DelayReasonResponseDto>>(path, "api/v1/DelayReason/GetByName", obj);
        if (result is not null)
            return new GenericCommandResult<IEnumerable<DelayReasonResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<IEnumerable<DelayReasonResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DelayReasonResponseDto>> Post(DelayReasonDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DelayReasonResponseDto>(path, "api/v1/DelayReason/Post", obj);
        if (result is not null)
            return new GenericCommandResult<DelayReasonResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DelayReasonResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<DelayReasonResponseDto>> Put(DelayReasonDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<DelayReasonResponseDto>(path, "api/v1/DelayReason/Put", obj);
        if (result is not null)
            return new GenericCommandResult<DelayReasonResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<DelayReasonResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/DelayReason/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}