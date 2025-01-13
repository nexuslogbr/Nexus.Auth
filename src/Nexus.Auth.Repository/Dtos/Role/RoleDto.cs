using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Dtos.Menu;

namespace Nexus.Auth.Repository.Dtos.Role
{
    public class RoleDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool Mobile { get; set; }
        public required IList<GetById> Menus { get; set; }
    }

    public class RoleIdDto : RoleDto
    {
        public int Id { get; set; }
    }
}
