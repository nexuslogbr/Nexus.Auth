using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.DamageType;

public class DamageTypeDto
{
    [Required]
    public string Name { get; set; }
}

public class DamageTypePutDto : DamageTypeDto
{
    [Required]
    public int Id { get; set; }
}