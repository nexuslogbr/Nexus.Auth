using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos;

public class VpcItemDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int ModelId { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int ManufacturerId { get; set; }

    public bool HasMinQuantity { get; set; }
    public int? MinQuantity { get; set; }
    public string? AdditionalNotes { get; set; }
}