using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Auth.Domain.Entities
{
    public class Menu : EntityBase
    {

        [Column(TypeName = "varchar(150)")]
        public required string Name { get; set; }

        [DefaultValue(false)]
        public bool Mobile { get; set; }

        public virtual List<SubMenu> SubMenus { get; set; }
    }
}
