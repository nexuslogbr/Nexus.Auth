
namespace Nexus.Auth.Domain.Entities
{
    public class SubMenu : EntityBase
    {
        public required string Name { get; set; }
        public required string Link { get; set; }
        public bool Mobile { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
