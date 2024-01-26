using Nexus.Auth.Repository.Dtos.UploadFile;
using Nexus.Auth.Repository.Enums;
using System.ComponentModel;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceResponseDto
    {
        public int Id { get; set; }
        public string Chassi { get; set; }
        public string CustomerName { get; set; }
        public string RequesterName { get; set; }

        public List<OrderServiceServiceDto> Services { get; set; }

        public OrderServiceStatusEnum OrderStatus { get; set; }
    }
}
