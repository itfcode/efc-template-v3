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

        public void Delete((TKey1, TKey2) key, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync((TKey1, TKey2) key, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<(TKey1, TKey2)> keys, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity? Get((TKey1, TKey2) key, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetAsync((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity Update((TKey1, TKey2) key, Action<TEntity> updater, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}