namespace Nexus.Auth.Repository.Dtos.Sla;

public class SlaResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime ChangeDate { get; set; }
    public DateTime RegisterDate { get; set; }

    public int ServiceDeadline { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int ServiceTypeId { get; set; }
    public string ServiceTypeName { get; set; }
}