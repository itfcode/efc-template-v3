using ITFCode.Core.Common;
using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Readonly
{
    public abstract class EntityReadonlyBaseService<TDbContext, TEntity>
        : CoreBaseService, IEntityReadonlyService<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        #endregion

        #region Constructors

        public EntityReadonlyBaseService(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        #endregion

        #region Protected Methods 

        protected virtual TEntity? Get(object[] keyValues, bool asNoTracking = true)
        {
            try
            {
                var entity = DbSet.Find(keyValues);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual async Task<TEntity?> GetAsync(object[] keyValues, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await DbSet.FindAsync(keyValues, cancellationToken);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected IReadOnlyCollection<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true) 
        {
            var query = DbSet.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            return [.. query];
        }

        protected async Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            var query = DbSet.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        protected string[] GetKeyPropertyNames()
        {
            var primaryKey = DbSet.EntityType.FindPrimaryKey()
                ?? throw new Exception("Primary Key Not Found");

            return primaryKey.Properties.Select(x => x.Name).ToArray();
        }

        #endregion
    }
}