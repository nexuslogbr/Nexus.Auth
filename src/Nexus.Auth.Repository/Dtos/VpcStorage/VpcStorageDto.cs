using System.ComponentModel.DataAnnotations;
using Nexus.Auth.Repository.Enums;

namespace Nexus.Auth.Repository.Dtos.VpcStorage;

public class VpcStorageDto
{
    [Required]
    public StorageTypeEnum StorageType { get; set; }
    [Required]
    public int VpcItemId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public int? RequesterId { get; set; }
    public int? PlaceId { get; set; }
    public string? PlaceName { get; set; }
    [Required]
    public bool IsSameCustomer { get; set; }
    [Required]
    public int EntranceOrExitCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? ExitReasonId { get; set; }
    public string? AdditionalNotes { get; set; }
}
public class VpcStoragePutDto : VpcStorageDto
{
    [Required]
    public int Id { get; set; }
}