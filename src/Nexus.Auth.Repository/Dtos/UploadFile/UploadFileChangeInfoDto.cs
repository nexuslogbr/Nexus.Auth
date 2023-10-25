using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.UploadFile;

public class UploadFileChangeInfoDto
{
    public int ConcludedRegisters { get; set; }
    public int FailedRegisters { get; set; }
    public UploadStatusEnum Status { get; set; }
    public int FileId { get; set; }
}