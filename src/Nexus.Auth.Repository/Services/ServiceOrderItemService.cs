using Azure;
using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.ServiceOrder;
using Nexus.Auth.Repository.Dtos.ServiceOrderItem;
using Nexus.Auth.Repository.Params;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class ServiceOrderItemService : IServiceOrderItemService
{
    private readonly IAccessDataService _accessDataService;

    public ServiceOrderItemService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }
    public async Task<GenericCommandResult<PageList<ServiceOrderItemResponseDto>>> GetAll(ServiceOrderItemParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<ServiceOrderItemResponseDto>>(path, "api/v1/ServiceOrderItem/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<ServiceOrderItemResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<ServiceOrderItemResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceOrderItemResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceOrderItemResponseDto>(path, "api/v1/ServiceOrderItem/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<ServiceOrderItemResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceOrderItemResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ServiceOrderItemResponseDto>> UpdateChassisItems(ServiceOrderItemPutDto dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<ServiceOrderItemResponseDto>(path, "api/v1/ServiceOrderItem/UpdateChassisItems", dto);
        if (result is not null)
            return new GenericCommandResult<ServiceOrderItemResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ServiceOrderItemResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}