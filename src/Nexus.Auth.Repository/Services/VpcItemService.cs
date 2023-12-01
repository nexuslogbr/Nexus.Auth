﻿using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.VpcItem;
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

    public async Task<GenericCommandResult<PageList<VpcItemResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<VpcItemResponseDto>>(path, "api/v1/VpcItem/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<VpcItemResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<VpcItemResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VpcItemResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<VpcItemResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcItemResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/VpcItem/ChangeStatus", obj);
        if (result is not null)
            return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VpcItemResponseDto>> Post(VpcItemDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/Post", obj);
        if (result is not null)
            return new GenericCommandResult<VpcItemResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcItemResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VpcItemResponseDto>> Put(VpcItemDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcItemResponseDto>(path, "api/v1/VpcItem/Put", obj);
        if (result is not null)
            return new GenericCommandResult<VpcItemResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcItemResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/VpcItem/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public Task<GenericCommandResult<VpcItemResponseDto>> GetByName(GetByName dto, string path)
    {
        throw new NotImplementedException();
    }
}