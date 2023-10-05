using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Sla;

public class SlaDto
{
    public int ServiceDeadline { get; set; }
    public int? MaterialDeadline { get; set; }
    public int? DaysAfterDeadline { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int ServiceTypeId { get; set; }
}
public class SlaPutDto : SlaDto
{
    [Required] 
    public int Id { get; set; }
}