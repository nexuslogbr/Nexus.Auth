using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Model;

public class ModelDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int ManufacturerId { get; set; }
    public IEnumerable<ModelVdsDto> VdsList { get; set; }
}

public class ModelPutDto : ModelDto
{
    [Required]
    public int Id { get; set; }
}