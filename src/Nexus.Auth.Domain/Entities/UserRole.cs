using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Domain.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserRole() { }

        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
