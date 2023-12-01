using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.VpcStorage;
using Nexus.Auth.Repository.Dtos.Auth;

namespace Nexus.Auth.Repository.Services;

public class VpcStorageService : IVpcStorageService
{
    private readonly IAccessDataService _accessDataService;

    public VpcStorageService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public Task<GenericCommandResult<PageList<VpcStorageResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        throw new NotImplementedException();
    }

    public async Task<GenericCommandResult<PageList<VpcStorageFlatResponseDto>>> GetAllFlat(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<VpcStorageFlatResponseDto>>(path, "api/v1/VpcStorage/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<VpcStorageFlatResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<VpcStorageFlatResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VpcStorageResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcStorageResponseDto>(path, "api/v1/VpcStorage/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<VpcStorageResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcStorageResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        throw new NotImplementedException();
    }

    public async Task<GenericCommandResult<VpcStorageResponseDto>> Post(VpcStorageDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcStorageResponseDto>(path, "api/v1/VpcStorage/Post", obj);
        if (result is not null)
            return new GenericCommandResult<VpcStorageResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcStorageResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<VpcStorageResponseDto>> Put(VpcStorageDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<VpcStorageResponseDto>(path, "api/v1/VpcStorage/Put", obj);
        if (result is not null)
            return new GenericCommandResult<VpcStorageResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<VpcStorageResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/VpcStorage/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public Task<GenericCommandResult<VpcStorageResponseDto>> GetByName(GetByName dto, string path)
    {
        throw new NotImplementedException();
    }
}