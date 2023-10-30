
namespace Nexus.Auth.Domain.Entities
{
    public class RoleMenu : EntityBase
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
