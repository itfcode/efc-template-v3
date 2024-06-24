using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud
{
    public abstract class EntityRepository<TDbContext, TEntity, TKey> : EntityRepository<TDbContext, TEntity>,
            IEntityRerository<TEntity, TKey>
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
            where TDbContext : DbContext
    {
        #region Constructors

        public EntityRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityRerository Implementation

        public void Delete(TKey key, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TKey key, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<TKey> keys, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<TKey> keys, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity? Get(TKey key, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TKey key, Action<TEntity> updater, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
