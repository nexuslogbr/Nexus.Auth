namespace Nexus.Auth.Repository.Dtos.ServiceOrderItem;

public class ServiceOrderItemResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int RequesterId { get; set; }
    public string RequesterName { get; set; }
    public int ChassisId { get; set; }
    public string ChassisNumber { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? ConcludedDate { get; set; }
    public IEnumerable<ChassisServiceItemResponseDto> ChassisServiceItems { get; set; }
}