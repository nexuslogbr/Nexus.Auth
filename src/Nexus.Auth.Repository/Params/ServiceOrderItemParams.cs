using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Params;

public class ServiceOrderItemParams
{
    public int? ServiceOrderId { get; set; }
    public string? ServiceName { get; set; }
    public string? VpcItemName { get; set; }
    public string? DamageTypeName { get; set; }
    public string? Comment { get; set; }
    public ServiceOrderItemStatusEnum? Status { get; set; }
}