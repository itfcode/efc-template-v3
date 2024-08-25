using ITFCode.Core.Data.EntityConfigurations;
using ITFCode.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ITFCode.Core.Data.Identity.DataContext
{
    public abstract class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        #region Constructors

        public ApplicationIdentityDbContext(DbContextOptions options) : base(options) { }

        #endregion

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BuildModelConfigurations(modelBuilder, [typeof(EntityCoreConfig<>)], Assembly.GetExecutingAssembly().GetTypes());
        }

        protected virtual void BuildModelConfigurations(ModelBuilder modelBuilder, Type[] baseTypes, Type[] types)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
            ArgumentNullException.ThrowIfNull(baseTypes, nameof(baseTypes));
            ArgumentNullException.ThrowIfNull(types, nameof(types));

            types.Where(type => type.BaseType != null && type.BaseType.IsGenericType && !type.IsAbstract
                    && baseTypes.Contains(type.BaseType.GetGenericTypeDefinition()))
                .ToList()
                .ForEach(type =>
                {
                    dynamic instance = Activator.CreateInstance(type) ?? throw new NullReferenceException();
                    modelBuilder.ApplyConfiguration(instance);
                });
        }

        #endregion
    }
}