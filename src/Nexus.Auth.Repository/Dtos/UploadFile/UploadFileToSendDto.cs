
using Microsoft.AspNetCore.Http;
using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.UploadFile
{
    public class UploadFileToSendDto
    {
        public IFormFile File { get; set; }
        public UploadTypeEnum Type { get; set; }
    }
}
