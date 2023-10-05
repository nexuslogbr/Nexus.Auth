using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Manufacturer
{
    public class WmiDto
    {
        [MinLength(2, ErrorMessage = "Lenght invalid.")]
        [MaxLength(3, ErrorMessage = "Lenght invalid")]
        public required string WMI { get; set; }
    }
}
