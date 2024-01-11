using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.VpcItem;

public class VpcItemDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }

    public bool HasMinQuantity { get; set; }
    public int? MinQuantity { get; set; }
    public string? AdditionalNotes { get; set; }

    [Required]
    public int CategoryId { get; set; }


    public virtual IEnumerable<VpcItemRequesterDto> Requesters { get; set; }
    public virtual IEnumerable<VpcItemYearDto> Years { get; set; }
    public virtual IEnumerable<VpcItemModelDto> Models { get; set; }
}

public class VpcItemPutDto : VpcItemDto
{
    [Required]
    public int Id { get; set; }
}