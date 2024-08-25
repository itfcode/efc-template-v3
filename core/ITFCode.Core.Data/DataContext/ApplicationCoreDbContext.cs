using ITFCode.Core.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ITFCode.Core.Data.DataContext
{
    public abstract class ApplicationCoreDbContext : DbContext
    {
        #region Public Properties : DbSets

        #endregion

        #region Constructors

        public ApplicationCoreDbContext(DbContextOptions options) : base(options) { }

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