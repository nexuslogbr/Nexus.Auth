namespace Nexus.Auth.Repository.Dtos.Model;

public class ModelResponseDto
{
    public int Id { get; set; }
    public bool Blocked { get; set; }
    public DateTime ChangeDate { get; set; }
    public DateTime RegisterDate { get; set; }

    public string Name { get; set; }
    public int ManufacturerId { get; set; }
    public string Manufacturer { get; set; }
    public IEnumerable<ModelVdsResponseDto> VdsList { get; set; }
}