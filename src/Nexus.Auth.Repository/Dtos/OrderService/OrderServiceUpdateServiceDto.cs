using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceUpdateServiceDto
    {
        public required int Id { get; set; }
        public required OrderServiceServiceStatusEnum Status { get; set; }
        public string? Comment { get; set; }
        public int DamageTypeId { get; set; }
    }
}
