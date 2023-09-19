using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos;

public class RequesterDto
{
    [Required]
    public string Name { get; set; }
}

public class RequesterPutDto : RequesterDto
{
    [Required]
    public int Id { get; set; }
}