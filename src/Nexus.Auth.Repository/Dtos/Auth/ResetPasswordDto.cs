using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.Auth
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string ResetPasswordToken { get; set; }
        public required string NewPassword { get; set; }
    }
}
