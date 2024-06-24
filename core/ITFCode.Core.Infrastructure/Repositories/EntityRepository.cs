using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ITFCode.Core.Infrastructure.Repositories
{
    public abstract class EntityRepository<TEntity, TDbContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Private Fields

        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Protected Properties 

        protected TDbContext DbContext => _dbContext ?? throw new NullReferenceException(nameof(_dbContext));
        protected DbSet<TEntity> DbSet => _dbSet ?? throw new NullReferenceException(nameof(_dbSet));

        #endregion

        #region Constructors

        public EntityRepository(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        #endregion

        #region IEntityRepository Implementation : Sync

        public virtual Task Delete(object id, CancellationToken cancellationToken = default)
        {
            return null;
        }

        public virtual Task<bool> Exist(object id, CancellationToken cancellationToken = default)
        {
            return null;
        }

        public virtual Task<TEntity> Get(object id, CancellationToken cancellationToken = default)
        {
            _dbSet.Find(id);

            return null;
        }

        public virtual Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {

            return null;
        }

        public virtual Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            return null;
        }

        #endregion

        #region IEntityRepository Implementation : Async 

        public virtual async Task<TEntity?> GetAsync([NotNull] object id, CancellationToken cancellationToken = default)
        {
            return id is null 
                ? throw new ArgumentNullException(nameof(id)) 
                : await DbSet.FindAsync([id], cancellationToken);
        }

        public virtual async Task<TEntity?> GetAsync([NotNull] object[] ids, CancellationToken cancellationToken = default)
        {
            return ids is null
                ? throw new ArgumentNullException(nameof(ids))
                : await DbSet.FindAsync(ids, cancellationToken);
        }

        public virtual Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {

            throw new NotImplementedException();
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
