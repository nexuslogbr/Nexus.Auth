using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceServiceResponseDto
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public int ServiceId { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        public string? Way { get; set; }

        public int? UserId { get; set; }
        public string? UserName { get; set; }

        public string? Comment { get; set; }
        public bool Damaged { get; set; }

        public OrderServiceServiceStatusEnum Status { get; set; }
    }
}
