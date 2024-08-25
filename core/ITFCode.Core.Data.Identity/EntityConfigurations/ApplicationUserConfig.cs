using ITFCode.Core.Data.EntityConfigurations;
using ITFCode.Core.Domain.Identity.Entities;

namespace RlyNetApplication.Core.Domain.EntityConfigurations.Identity
{
    public  class ApplicationUserConfig : EntityCoreConfig<ApplicationUser>
    {
        protected override void Configure()
        {
            base.Configure();

            Builder.Property(x => x.FirstName).HasMaxLength(99);
            Builder.Property(x => x.LastName).HasMaxLength(99);

            // Each User can have many UserClaims
            Builder.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            Builder.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            Builder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            Builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}