using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Data.EntityConfigurations
{
    /// <summary>
    /// Base Class for configuration of Entity
    /// </summary>
    /// <typeparam name="TEntity"> Entity Class</typeparam>
    public abstract class EntityCoreConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        #region Private Fields 

        private EntityTypeBuilder<TEntity>? _builder;

        #endregion

        #region Protected Properties

        protected virtual string TableName => typeof(TEntity).Name + "s";

        protected EntityTypeBuilder<TEntity> Builder => _builder ?? throw new NullReferenceException("Builder Not Defined");

        #endregion

        #region Public Methods: IEntityTypeConfiguration Implementation

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            _builder = builder;

            Configure();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Configures Entity 
        /// </summary>
        protected virtual void Configure() { }

        #endregion
    }
}