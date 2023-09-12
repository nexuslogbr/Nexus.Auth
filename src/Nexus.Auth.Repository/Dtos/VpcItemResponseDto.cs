namespace Nexus.Auth.Repository.Dtos;

public class VpcItemResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime ChangeDate { get; set; }
    public DateTime RegisterDate { get; set; }

    public string Name { get; set; }
    public int ModelId { get; set; }
    public int Year { get; set; }
    public int CategoryId { get; set; }
    public int ManufacturerId { get; set; }
    public bool HasMinQuantity { get; set; }
    public int MinQuantity { get; set; }
    public string AdditionalNotes { get; set; }
}