using Nexus.Auth.Repository.Dtos.Role;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Blocked { get; set; }
        public IList<RoleUserDto> Roles { get; set; }
    }

    public class UserIdDto : UserDto
    {
        public int Id { get; set; }
    }
}
