using Nexus.Auth.Repository.Dtos.Role;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Repository.Dtos.User
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int PlaceId { get; set; }
        public IList<RoleUserDto>? Roles { get; set; }
        public bool Mobile { get; set; }
    }

    public class UserIdDto : UserDto
    {
        public int Id { get; set; }
    }
}
