using Microsoft.AspNetCore.Http;
using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.OrderService;

namespace Nexus.Auth.Repository.Dtos.UploadFile;

public class UploadFileDto
{
    public IFormFile File { get; set; }
    public UploadTypeEnum Type { get; set; }
    public int ConcludedRegisters { get; set; }
    public int FailedRegisters { get; set; }
    public List<OrderServiceDto>? OrderService { get; set; }
}