namespace Nexus.Auth.Repository.Dtos.Service;

public class ServiceResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Observation { get; set; }
    public IEnumerable<ServiceRelatedCustomerResponseDto> ServiceRelatedCustomers { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
}