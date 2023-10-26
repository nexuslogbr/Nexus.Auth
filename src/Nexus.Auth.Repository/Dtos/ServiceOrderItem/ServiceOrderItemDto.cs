namespace Nexus.Auth.Repository.Dtos.ServiceOrderItem;

public class ServiceOrderItemDto
{
    public int ChassisId { get; set; }
    public string? ChassisNumber { get; set; }
    public int? ModelId { get; set; }
    public IEnumerable<ChassisServiceItemDto> ChassisServiceItems { get; set; }
}