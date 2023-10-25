using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.UploadFile;

public class UploadFileResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ConcludedRegisters { get; set; }
    public int FailedRegisters { get; set; }
    public UploadStatusEnum Status { get; set; }
    public UploadTypeEnum Type { get; set; }
    public IEnumerable<string> Data { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}