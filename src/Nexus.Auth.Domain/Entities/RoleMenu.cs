
namespace Nexus.Auth.Domain.Entities
{
    public class RoleMenu : EntityBase
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
