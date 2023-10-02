using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class CategoryService : ICategoryService
{
    private readonly IAccessDataService _accessDataService;

    public CategoryService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<CategoryResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<CategoryResponseDto>>(path, "api/v1/Category/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<CategoryResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<CategoryResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<CategoryResponseDto>> GetById(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<CategoryResponseDto>(path, "api/v1/Category/GetById", obj);
        if (result is not null)
            return new GenericCommandResult<CategoryResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<CategoryResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<CategoryResponseDto>> Post(CategoryDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<CategoryResponseDto>(path, "api/v1/Category/Post", obj);
        if (result is not null)
            return new GenericCommandResult<CategoryResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<CategoryResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<CategoryResponseDto>> Put(CategoryDto obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<CategoryResponseDto>(path, "api/v1/Category/Put", obj);
        if (result is not null)
            return new GenericCommandResult<CategoryResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<CategoryResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/Category/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}