using Nexus.Auth.Repository.Dtos.UploadFile;

namespace Nexus.Auth.Repository.Dtos.FileVpc
{
    public class FileVpcDto
    {
        public string? Place { get; set; }
        public string? Customer { get; set; }
        public string? RequesterCode { get; set; }
        public string? Requester { get; set; }
        public string? Chassis { get; set; }
        public DateTime? Invoicing { get; set; }
        public List<FileVpcServiceDto>? Services { get; set; }
        public string? Street { get; set; }
        public int? Parking { get; set; }
        public string? Plate { get; set; }
        public int UploadFileId { get; set; }

        public bool Blocked { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ChangeDate { get; set; }

        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
