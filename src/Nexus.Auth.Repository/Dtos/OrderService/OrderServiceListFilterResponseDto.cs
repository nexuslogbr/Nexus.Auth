namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceListFilterResponseDto
    {
        public int Id { get; set; }
        public string[] Requesters { get; set; }
        public string[] Services { get; set; }
        public int ChassisCount { get; set; }
        public int ServicesCount { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
