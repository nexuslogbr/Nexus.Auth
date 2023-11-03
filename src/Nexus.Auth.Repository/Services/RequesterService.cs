using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Requester;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class RequesterService : IRequesterService
{
    private readonly IAccessDataService _accessDataService;

    public RequesterService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<RequesterResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<RequesterResponseDto>>(path, "api/v1/Requester/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<RequesterResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<RequesterResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<RequesterResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<RequesterResponseDto>(path, "api/v1/Requester/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<RequesterResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<RequesterResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Requester/ChangeStatus", obj);
        if (result is not null)
            return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<RequesterResponseDto>> Post(RequesterDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<RequesterResponseDto>(path, "api/v1/Requester/Post", obj);
        if (result is not null)
            return new GenericCommandResult<RequesterResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<RequesterResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<RequesterResponseDto>> Put(RequesterDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<RequesterResponseDto>(path, "api/v1/Requester/Put", obj);
        if (result is not null)
            return new GenericCommandResult<RequesterResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<RequesterResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Requester/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}