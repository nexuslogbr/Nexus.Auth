using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
