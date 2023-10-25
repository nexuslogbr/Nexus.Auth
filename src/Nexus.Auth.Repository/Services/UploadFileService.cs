using Microsoft.AspNetCore.Http;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Category;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Services.Interfaces;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IAccessDataService _accessDataService;

    public UploadFileService(IAccessDataService accessDataService)
    {
        _accessDataService = accessDataService;
    }

    public async Task<GenericCommandResult<PageList<UploadFileResponseDto>>> GetAll(PageParams pageParams, string path)
    {
        var result = await _accessDataService.PostDataAsync<PageList<UploadFileResponseDto>>(path, "api/v1/UploadFile/GetAll", pageParams);
        if (result is not null)
            return new GenericCommandResult<PageList<UploadFileResponseDto>>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<PageList<UploadFileResponseDto>>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> GetById(GetById dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/GetById", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path)
    {
        var result = await _accessDataService.PostDataAsync<TokenDto>(path, "api/v1/UploadFile/Delete", obj);
        if (result is not null)
            return new GenericCommandResult<TokenDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<TokenDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> Post(UploadFileDto dto, string path)
    {
        var result = await _accessDataService.PostFormDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/Post", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }

    public async Task<GenericCommandResult<UploadFileResponseDto>> ChangeInfo(UploadFileChangeInfoDto dto, string path)
    {
        var result = await _accessDataService.PostDataAsync<UploadFileResponseDto>(path, "api/v1/UploadFile/ChangeInfo", dto);
        if (result is not null)
            return new GenericCommandResult<UploadFileResponseDto>(true, "Success", result, StatusCodes.Status200OK);

        return new GenericCommandResult<UploadFileResponseDto>(true, "Error", default, StatusCodes.Status400BadRequest);
    }
}