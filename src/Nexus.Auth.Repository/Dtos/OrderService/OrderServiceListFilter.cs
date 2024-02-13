namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceListFilter
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int RequesterId { get; set; }
        public string? Chassi { get; set; }
        public int ServiceQuantity { get; set; }
        public int ChassiQuantity { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
