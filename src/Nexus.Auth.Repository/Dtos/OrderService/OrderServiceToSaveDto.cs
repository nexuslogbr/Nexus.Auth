using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceToSaveDto
    {
        public string Chassi { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int RequesterId { get; set; }
        public string RequesterName { get; set; }
        public string RequesterCode { get; set; }

        public List<OrderServicesDto> Services { get; set; }

        public OrderServiceStatusEnum OrderStatus { get; set; }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }

        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public DateTime Invoicing { get; set; }
        public string Street { get; set; }
        public int Parking { get; set; }
        public string Plate { get; set; }

        public int FileId { get; set; }
        public string? Place { get; set; }
    }

    public class OrderServiceToSavePutDto : OrderServiceToSaveDto
    {
        public int Id { get; set; }
    }
}
