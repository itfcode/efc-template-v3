using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud
{
    public abstract class EntityRepository<TDbContext, TEntity, TKey1, TKey2> : EntityRepository<TDbContext, TEntity>,
            IEntityRepository<TEntity, TKey1, TKey2>
            where TEntity : class, IEntity<TKey1, TKey2>
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
            where TDbContext : DbContext
    {
        #region Constructors

        public EntityRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityRerository Implementation

        public virtual TEntity? Get((TKey1, TKey2) key)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys,  CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update((TKey1, TKey2) key, Action<TEntity> updater)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater,  CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete((TKey1, TKey2) key)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteRange(IEnumerable<(TKey1, TKey2)> keys)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}