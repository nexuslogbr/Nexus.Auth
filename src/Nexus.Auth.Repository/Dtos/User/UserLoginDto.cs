namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserLoginDto
    {
        public string UserName { get; set; }
        public required string Password { get; set; }
    }
}
