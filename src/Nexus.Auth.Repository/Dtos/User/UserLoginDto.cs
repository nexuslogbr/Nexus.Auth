namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public required string Password { get; set; }
    }
}
