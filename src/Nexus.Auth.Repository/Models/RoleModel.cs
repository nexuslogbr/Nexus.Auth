using Nexus.Auth.Repository.Dtos;

namespace Nexus.Auth.Repository.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Blocked { get; set; }
        public string ChangeDate { get; set; }
        public IList<MenuIdDto> Menus { get; set; }
    }
}
