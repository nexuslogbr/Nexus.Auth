using Nexus.Auth.Repository.Dtos.UploadFile;
using System.ComponentModel;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceDto
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
        public int? UploadFileId { get; set; }

        [DefaultValue(true)]
        public bool Success { get; set; }
        public string Error { get; set; }

        public int RequesterId { get; set; }
        public int CustomerId { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int PlaceId { get; set; }
    }
}
