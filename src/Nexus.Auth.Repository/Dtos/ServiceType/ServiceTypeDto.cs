using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.ServiceType;

public class ServiceTypeDto
{

    [Required]
    public string Name { get; set; }
}

public class ServiceTypePutDto : ServiceTypeDto
{
    [Required]
    public int Id { get; set; }
}