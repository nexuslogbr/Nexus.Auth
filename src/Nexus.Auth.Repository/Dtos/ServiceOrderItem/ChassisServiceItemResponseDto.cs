using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.ServiceOrderItem;

public class ChassisServiceItemResponseDto
{
    public int Id { get; set; }
    public ServiceOrderItemStatusEnum Status { get; set; }
    public string? DamageTypeName { get; set; }
    public int? DamageTypeId { get; set; }
    public string? Comment { get; set; }
    public int? DaysAfterDeadline { get; set; }
    public string? Justification { get; set; }
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public int VpcItemId { get; set; }
    public string VpcItemName { get; set; }
}