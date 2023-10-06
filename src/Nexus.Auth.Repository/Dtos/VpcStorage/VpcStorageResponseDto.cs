using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.VpcStorage;

public class VpcStorageResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime ChangeDate { get; set; }
    public StorageTypeEnum StorageType { get; set; }
    public int VpcItemId { get; set; }
    public string VpcItemName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int RequesterId { get; set; }
    public string RequesterName { get; set; }
    public int StorageCount { get; set; }
    public bool IsSameCustomer { get; set; }
    public int EntranceOrExitCount { get; set; }
    public decimal UnitPrice { get; set; }
    public int ExitReasonId { get; set; }
    public string AdditionalNotes { get; set; }
}