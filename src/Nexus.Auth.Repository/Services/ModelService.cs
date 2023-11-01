using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;
using Nexus.Auth.Repository.Dtos.Model;
using Nexus.Auth.Repository.Dtos.Auth;

namespace Nexus.Auth.Repository.Services;

public class ModelService : IModelService
{
    private readonly IAccessDataService _accessDataService;

    public ModelService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<ModelResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<ModelResponseDto>>(path, "api/v1/Model/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<ModelResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<ModelResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ModelResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelResponseDto>(path, "api/v1/Model/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<ModelResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ModelResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<IEnumerable<ModelResponseDto>>> GetByManufacturerId(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<ModelResponseDto>>(path, "api/v1/Model/GetByManufacturerId", obj);
        if (result is not null)
            return new GenericCommandResult<IEnumerable<ModelResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<IEnumerable<ModelResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ChangeStatusDto>> ChangeStatus(ChangeStatusDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ChangeStatusDto>(path, "api/v1/Model/ChangeStatus", obj);
        if (result is not null)
            return new GenericCommandResult<ChangeStatusDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ChangeStatusDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ModelResponseDto>> Post(ModelDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelResponseDto>(path, "api/v1/Model/Post", obj);
        if (result is not null)
            return new GenericCommandResult<ModelResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ModelResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<ModelResponseDto>> Put(ModelDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelResponseDto>(path, "api/v1/Model/Put", obj);
        if (result is not null)
            return new GenericCommandResult<ModelResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<ModelResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Model/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}