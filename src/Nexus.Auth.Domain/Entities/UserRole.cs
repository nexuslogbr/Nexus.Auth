using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Auth.Domain.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserRole() { }

        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
