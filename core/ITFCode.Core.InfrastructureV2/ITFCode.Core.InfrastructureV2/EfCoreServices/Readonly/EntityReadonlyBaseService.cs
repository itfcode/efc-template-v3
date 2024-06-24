using ITFCode.Core.Common;
using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV2.EfCore.Interfaces.Readers;
using ITFCode.Core.InfrastructureV2.EfCore.Readers;
using ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Readonly;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Readonly
{
    public abstract class EntityReadonlyBaseService<TDbContext, TEntity> : CoreBaseService, IEntityReadonlyService<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        internal IEfDataReader<TEntity> DataReader { get; }

        #endregion

        #region Constructors

        public EntityReadonlyBaseService(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
            DataReader = new EfDataReader<TDbContext, TEntity>(dbContext);
        }

        #endregion

        #region IEntityReadonlyService<TEntity> Implementation

        public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
            => DbSet.FirstOrDefault(predicate);

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        #endregion
        
        #region Protected Methods 

        protected string[] GetKeyPropertyNames()
        {
            var primaryKey = DbSet.EntityType.FindPrimaryKey()
                ?? throw new Exception("Primary Key Not Found");

            return primaryKey.Properties.Select(x => x.Name).ToArray();
        }

        #endregion
    }
}
