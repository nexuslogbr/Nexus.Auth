namespace Nexus.Auth.Repository.Dtos;

public class VpcItemResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime ChangeDate { get; set; }
    public DateTime RegisterDate { get; set; }

    public string Name { get; set; }
    public bool HasMinQuantity { get; set; }
    public int MinQuantity { get; set; }
    public string AdditionalNotes { get; set; }

    public int ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public virtual IEnumerable<VpcItemYearResponseDto> Years { get; set; }
    public virtual IEnumerable<VpcItemModelResponseDto> Models { get; set; }
}