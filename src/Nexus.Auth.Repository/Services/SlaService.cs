using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Sla;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class SlaService : ISlaService
{
    private readonly IAccessDataService _accessDataService;

    public SlaService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<SlaResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<SlaResponseDto>>(path, "api/v1/Sla/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<SlaResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<SlaResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<SlaResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<SlaResponseDto>(path, "api/v1/Sla/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<SlaResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<SlaResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Sla/ChangeStatus", obj);
        if (result is not null)
            return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<SlaResponseDto>> Post(SlaDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<SlaResponseDto>(path, "api/v1/Sla/Post", obj);
        if (result is not null)
            return new GenericCommandResult<SlaResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<SlaResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<SlaResponseDto>> Put(SlaDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<SlaResponseDto>(path, "api/v1/Sla/Put", obj);
        if (result is not null)
            return new GenericCommandResult<SlaResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<SlaResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Sla/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

}