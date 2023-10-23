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
    Task<GenericCommandResult<UploadFileResponseDto>> ChangeStatus(UploadFileChangeStatusDto dto, string path);
}