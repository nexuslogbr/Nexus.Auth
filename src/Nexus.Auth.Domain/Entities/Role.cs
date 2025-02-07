using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Auth.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        private DateTime _registerDate = DateTime.Now;

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool Blocked { get; set; }

        public bool Mobile { get; set; }

        public DateTime RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public DateTime ChangeDate { get; set; }

        public virtual IList<UserRole>? UserRoles { get; set; }
        public virtual IList<RoleMenu>? RoleMenus { get; set; }

        [NotMapped]
        public virtual IList<Menu>? Menus { get; set; }

        public int PlaceData { get; set; }
    }
}
