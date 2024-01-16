using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.ServiceType;
using Nexus.Auth.Repository.Dtos.Auth;

namespace Nexus.Auth.Repository.Services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IAccessDataService _accessDataService;

    public ServiceTypeService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<ServiceTypeResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<ServiceTypeResponseDto>>(path, "api/v1/ServiceType/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<ServiceTypeResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<ServiceTypeResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceTypeResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceTypeResponseDto>(path, "api/v1/ServiceType/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/ServiceType/ChangeStatus", obj);
        if (result is not null)
            return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceTypeResponseDto>> Post(ServiceTypeDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceTypeResponseDto>(path, "api/v1/ServiceType/Post", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceTypeResponseDto>> Put(ServiceTypeDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceTypeResponseDto>(path, "api/v1/ServiceType/Put", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceTypeResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceTypeResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/ServiceType/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public Task<GenericCommandResult<ServiceTypeResponseDto>> GetByName(GetByName dto, string path)
    {
        throw new NotImplementedException();
    }
}