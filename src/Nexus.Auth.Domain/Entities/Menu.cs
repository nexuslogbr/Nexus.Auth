﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Auth.Domain.Entities
{
    public class Menu : EntityBase
    {
        [Key]
        public required int Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        public required string Name { get; set; }

        [DefaultValue(false)]
        public bool Mobile { get; set; }

    }
}
