using Nexus.Auth.Repository.Dtos.Place;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserResult
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public required string ResetPasswordToken { get; set; }
        public required PlaceResponseDto Location { get; set; }
    }
}
