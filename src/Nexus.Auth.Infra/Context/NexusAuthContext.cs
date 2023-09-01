using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Auth.Infra.Context
{
    public class NexusAuthContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public NexusAuthContext(DbContextOptions<NexusAuthContext> options) : base(options) { }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(u => new { u.UserId, u.RoleId });
                userRole.HasOne(r => r.Role).WithMany(x => x.UserRoles).HasForeignKey(s => s.RoleId).IsRequired();
                userRole.HasOne(r => r.User).WithMany(x => x.UserRoles).HasForeignKey(s => s.UserId).IsRequired();
            });

            //Especifica a relação de n para n e definindo quais são os identificadores
            modelBuilder.Entity<RoleMenu>().HasKey(p => new { p.RoleId, p.MenuId });
        }
    }
}
