using System.ComponentModel.DataAnnotations;

namespace Auth.Repository.Dtos.Generics
{
    public class RequestResetPasswordDto
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
