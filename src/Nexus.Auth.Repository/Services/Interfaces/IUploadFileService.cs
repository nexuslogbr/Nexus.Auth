using Microsoft.AspNetCore.Http;
using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.Auth;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Utils;

namespace Nexus.Auth.Repository.Services.Interfaces;

public interface IUploadFileService
{
    Task<GenericCommandResult<PageList<UploadFileResponseDto>>> GetAll(PageParams pageParams, string path);
    Task<GenericCommandResult<UploadFileResponseDto>> GetById(GetById dto, string path);
    Task<GenericCommandResult<TokenDto>> Delete(GetById obj, string path);
    Task<GenericCommandResult<UploadFileResponseDto>> Post(UploadFileDto dto, string path);
    Task<GenericCommandResult<UploadFileResponseDto>> ChangeInfo(UploadFileChangeInfoDto dto, string path);
    IEnumerable<UploadFileResult> GetFileData(IFormFile formFile, UploadTypeEnum type);
}