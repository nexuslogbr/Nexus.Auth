using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceResponseDto
    {
        public int Id { get; set; }
        public string Chassi { get; set; }
        public string CustomerName { get; set; }
        public string RequesterName { get; set; }
        public string RequesterCode { get; set; }
        public string ManufacturerName { get; set; }
        public string ModelName { get; set; }

        public List<OrderServiceServiceResponseDto> Services { get; set; }

        public OrderServiceStatusEnum OrderStatus { get; set; }

        public DateTime Invoicing { get; set; }
        public string Street { get; set; }
        public int Parking { get; set; }
        public string Plate { get; set; }


        public int? AgingDays { get; set; }
        public DateTime? VpcDateEnding { get; set; }
        public string? Transporter { get; set; }
        public DateTime? ExpeditionDate { get; set; }

        public int? InvoicingUntilDispatchDays { get; set; }
    }
}
