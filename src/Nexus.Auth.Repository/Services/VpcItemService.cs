using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class VpcItemService : IVpcItemService
{
    private readonly IAccessDataService _accessDataService;

    public VpcItemService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<VpcItemResponseDto>>(path, "api/v1/VpcItem/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/GetById", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> GetByName(GetByName obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<VpcItemResponseDto>>(path, "api/v1/VpcItem/GetByName", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Post(VpcItemDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/Post", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Put(VpcItemDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/Put", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/VpcItem/Delete", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }
}