using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.ServiceOrder;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class ServiceOrderService : IServiceOrderService
{
    private readonly IAccessDataService _accessDataService;

    public ServiceOrderService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<ServiceOrderResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<ServiceOrderResponseDto>>(path, "api/v1/ServiceOrder/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<ServiceOrderResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<ServiceOrderResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceOrderResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceOrderResponseDto>(path, "api/v1/ServiceOrder/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceOrderResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceOrderResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceOrderResponseDto>> Post(ServiceOrderDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceOrderResponseDto>(path, "api/v1/ServiceOrder/Post", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceOrderResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceOrderResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceOrderResponseDto>> Put(ServiceOrderDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceOrderResponseDto>(path, "api/v1/ServiceOrder/Put", obj);
        if (result is not null)
            return new GenericCommandResult<ServiceOrderResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceOrderResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/ServiceOrder/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}