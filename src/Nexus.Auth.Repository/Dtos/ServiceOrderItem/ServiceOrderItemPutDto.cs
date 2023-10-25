using System.ComponentModel.DataAnnotations;
using Nexus.Auth.Domain.Enums;

namespace Nexus.Auth.Repository.Dtos.ServiceOrderItem;

public class ServiceOrderItemPutDto
{
    [Required]
    public int Id { get; set; }
    public IEnumerable<int> ItemIds { get; set; }
    [Required]
    public ServiceOrderItemStatusEnum Status { get; set; }
    public int? DaysAfterDeadline { get; set; }
    [Required]
    public string Justification { get; set; }
}