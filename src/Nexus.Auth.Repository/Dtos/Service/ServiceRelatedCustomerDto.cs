namespace Nexus.Auth.Repository.Dtos.Service;

public class ServiceRelatedCustomerDto
{
    public decimal Value { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public int ServiceTypeId { get; set; }
}