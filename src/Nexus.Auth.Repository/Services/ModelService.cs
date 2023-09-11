using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Models;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class ModelService : IModelService
{
    private readonly IAccessDataService _accessDataService;

    public ModelService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<ModelDto>>(path, "api/v1/Model/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelDto>(path, "api/v1/Model/GetById", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> GetByName(GetByName obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<IEnumerable<ModelDto>>(path, "api/v1/Model/GetByName", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Post(ModelDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelDto>(path, "api/v1/Model/Post", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Put(ModelDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<ModelDto>(path, "api/v1/Model/Put", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Model/Delete", obj);
        if (result is not null)
            return new GenericCommandResult(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult(true, "Error", new object { }, StatusCodes.Status400BadRequest);
    }
}