using Microsoft.AspNetCore.Http;
using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.UploadFile;

public class UploadFileDto
{
    public IFormFile File { get; set; }
    public UploadTypeEnum Type { get; set; }
    public int ConcludedRegisters { get; set; }
    public int FailedRegisters { get; set; }
}