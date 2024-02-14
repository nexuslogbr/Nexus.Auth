namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceListResponseDto
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public required List<OrderServiceServicesListResponseDto> ListServices { get; set; }
    }
}
