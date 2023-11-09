using Nexus.Auth.Repository.Dtos.Role;

namespace Nexus.Auth.Repository.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Blocked { get; set; }
        public DateTime ChangeDate { get; set; }
        public IList<RoleUserDto> Roles { get; set; }
    }
}
