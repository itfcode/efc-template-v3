using ITFCode.Core.Domain.Tests.TestKit.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.TestKit
{
    public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
    {
        #region Public Properties: DbSets

        public DbSet<UserTc> Users { get; set; }
        public DbSet<RoleTc> Roles { get; set; }
        public DbSet<UserRoleTc> UserRoles { get; set; }
        public DbSet<ProductTc> Products { get; set; }
        public DbSet<LocationTc> Locations { get; set; }
        public DbSet<CompanyTc> Companies { get; set; }
        public DbSet<ProductOrderTc> ProductOrders { get; set; }

        #endregion

        #region Protected Methods 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTc>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserTc>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .IsRequired()
                .HasForeignKey(ur => ur.UserId)
                .HasPrincipalKey(user => user.Id);

            modelBuilder.Entity<RoleTc>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<RoleTc>()
                .HasMany(u => u.UserRoles)
                .WithOne(x => x.Role)
                .IsRequired()
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(role => role.Id);

            modelBuilder.Entity<UserRoleTc>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<ProductOrderTc>()
                .HasKey(ur => new { ur.CompanyId, ur.LocationId, ur.UserId });
        }

        #endregion
    }
}