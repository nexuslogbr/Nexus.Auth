using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nexus.Auth.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Auth.Domain.Entities
{
    public class Menu : EntityBase
    {
        [Column(TypeName = "varchar(150)")]
        public required string Name { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public bool Mobile { get; set; }
        public MenuTypeEnum Type { get; set; }

        public int? MenuSectionId { get; set; }
    }
}
