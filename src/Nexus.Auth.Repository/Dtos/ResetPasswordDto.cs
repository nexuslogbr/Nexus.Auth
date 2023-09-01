using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }
    }
}
