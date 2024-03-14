using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Nexus.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NexusAuthContext).Assembly);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(u => new { u.UserId, u.RoleId });
                userRole.HasOne(r => r.Role).WithMany(x => x.UserRoles).HasForeignKey(s => s.RoleId).IsRequired();
                userRole.HasOne(r => r.User).WithMany(x => x.UserRoles).HasForeignKey(s => s.UserId).IsRequired();
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.RegisterDate = DateTime.Now;
                        entityEntry.Entity.ChangeDate = DateTime.Now;
                        entityEntry.Entity.Blocked = false;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.ChangeDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }

    public abstract class EntityBaseMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
