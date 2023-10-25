namespace Nexus.Auth.Repository.Dtos.ServiceOrderItem;

public class ServiceOrderItemFlatResponse
{
    public int Id { get; set; }
    public int ChassisId { get; set; }
    public string ChassisNumber { get; set; }
    public int ServiceCount { get; set; }
    public int ConcludedServiceCount { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public DateTime? ConcludedDate { get; set; }
}