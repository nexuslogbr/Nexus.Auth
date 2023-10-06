using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos;

public class DelayReasonDto
{
    [Required] 
    public string Name { get; set; }
}

public class DelayReasonPutDto : DelayReasonDto
{
    [Required] 
    public int Id { get; set; }
}