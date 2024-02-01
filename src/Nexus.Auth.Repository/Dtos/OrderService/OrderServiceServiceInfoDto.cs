using Nexus.Auth.Repository.Dtos.Service;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceServiceInfoDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public ServiceResponseDto Service { get; set; }
    }
}
