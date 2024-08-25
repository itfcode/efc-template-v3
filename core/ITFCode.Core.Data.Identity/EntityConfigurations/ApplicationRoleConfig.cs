using ITFCode.Core.Data.EntityConfigurations;
using ITFCode.Core.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace RlyNetApplication.Core.Domain.EntityConfigurations.Identity
{
    public class ApplicationRoleConfig : EntityCoreConfig<ApplicationRole> 
    {
        protected override void Configure()
        {
            base.Configure();

            Builder.Property(x => x.Description)
                .HasMaxLength(499)
                .HasColumnOrder(4);

            // Each Role can have many entries in the UserRole join table
            Builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            Builder.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }
    }
}