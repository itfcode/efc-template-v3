using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud
{
    public abstract class EntityRepository<TDbContext, TEntity, TKey> : EntityRepository<TDbContext, TEntity>,
            IEntityRepository<TEntity, TKey>
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
    {
        #region Constructors

        public EntityRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityRerository Implementation

        public virtual TEntity? Get(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TKey key, Action<TEntity> updater)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteRange(IEnumerable<TKey> keys)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
