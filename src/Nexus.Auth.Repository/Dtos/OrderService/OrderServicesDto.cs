using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServicesDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public OrderServiceServiceStatusEnum Status { get; set; }
    }
}
