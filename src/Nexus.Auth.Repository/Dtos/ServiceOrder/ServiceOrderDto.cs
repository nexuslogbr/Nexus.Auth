using System.ComponentModel.DataAnnotations;
using Nexus.Auth.Repository.Dtos.ServiceOrderItem;

namespace Nexus.Auth.Repository.Dtos.ServiceOrder;

public class ServiceOrderDto
{
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }

    public int RequesterId { get; set; }

    public virtual IEnumerable<ServiceOrderItemDto> OrderItems { get; set; }
}

public class ServiceOrderPutDto : ServiceOrderDto
{
    [Required]
    public int Id { get; set; }
}