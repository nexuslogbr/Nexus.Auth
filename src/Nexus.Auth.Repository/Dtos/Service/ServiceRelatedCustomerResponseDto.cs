namespace Nexus.Auth.Repository.Dtos.Service;

public class ServiceRelatedCustomerResponseDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int ServiceTypeId { get; set; }
    public string ServiceTypeName { get; set; }
}