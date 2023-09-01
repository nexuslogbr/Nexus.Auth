using Nexus.Auth.Repository.Dtos.Generics;

namespace Nexus.Auth.Repository.Dtos
{
    public class RoleDto
    {
        public required string Name { get; set; }
        public string Description { get; set; }
        public IList<GetById> Menus { get; set; }
    }

    public class RoleIdDto : RoleDto
    {
        public int Id { get; set; }
    }
}
