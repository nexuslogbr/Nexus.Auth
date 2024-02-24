using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.Auth.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        private DateTime _registerDate = DateTime.Now;

        [Column(TypeName = "varchar(150)")]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool Blocked { get; set; }

        public DateTime RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        public DateTime ChangeDate { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public bool Mobile { get; set; }
        public int? PlaceId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? PlaceName { get; set; }

        [NotMapped]
        public IList<Role> Roles { get; set; }
        [NotMapped]
        public string ResetPasswordToken { get; set; }
    }
}
