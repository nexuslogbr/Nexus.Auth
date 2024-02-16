namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceUpdateDto
    {
        public required int Id { get; set; }
        public bool Mobile { get; set; }
        public required List<OrderServiceUpdateServiceDto> Services { get; set; }
    }
}
