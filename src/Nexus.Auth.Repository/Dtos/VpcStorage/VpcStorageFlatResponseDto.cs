namespace Nexus.Auth.Repository.Dtos.VpcStorage;

public class VpcStorageFlatResponseDto
{
    public int Id { get; set; }
    public int VpcItemId { get; set; }
    public string VpcItemName { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int RequesterId { get; set; }
    public string RequesterName { get; set; }
    public int StorageCount { get; set; }
    public DateTime ChangeDate { get; set; }
}