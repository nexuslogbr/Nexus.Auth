using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.UploadFile;

public class UploadFileChangeStatusDto
{
    public UploadStatusEnum Status { get; set; }
    public int FileId { get; set; }
}