using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceServicesListResponseDto
    {
        public int Id { get; set; }
        public required string Requester { get; set; }
        public required string Chassi { get; set; }
        public required string Service { get; set; }
        public int Parking { get; set; }
        public required string Street { get; set; }
        public OrderServiceServiceStatusEnum Status { get; set; }
    }
}
