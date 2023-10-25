using Nexus.Auth.Domain.Enums;
using Nexus.Auth.Repository.Dtos.ServiceOrderItem;

namespace Nexus.Auth.Repository.Dtos.ServiceOrder;

public class ServiceOrderResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
    public string ServiceOrderNumber { get; set; }
    public ServiceOrderStatusEnum Status { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int RequesterId { get; set; }
    public string RequesterName { get; set; }
    public int ChassisCount { get; set; }
    public int ServiceCount { get; set; }
    public int VpcItemCount { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? ConcludedDate { get; set; }
    public IEnumerable<ServiceOrderItemFlatResponse> OrderItems { get; set; }
}