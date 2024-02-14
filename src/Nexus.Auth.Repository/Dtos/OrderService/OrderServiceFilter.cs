namespace Nexus.Auth.Repository.Dtos.OrderService
{
    public class OrderServiceFilter
    {
        public int? CustomerId { get; set; }
        public int? RequesterId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ModelId { get; set; }
        public List<string>? Chassis { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool CustomerKit { get; set; }
        public List<int>? QuantityOfServices { get; set; }
        public List<int>? ServiceList { get; set; }
    }
}
