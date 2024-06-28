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
            try
            {
                return DbReader.Get<TEntity>(key, asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetAsync<TEntity>(key, asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys)
        {
            try
            {
                return DbReader.GetMany<TEntity>(keys.Cast<object>(), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetManyAsync<TEntity>(keys.Cast<object>(), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Update(TKey key, Action<TEntity> updater)
        {
            try
            {
                return DbUpdater.Update(key, updater, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbUpdater.UpdateAsync(key, updater, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater)
        {
            try
            {
                DbUpdater.UpdateRange(keys.Cast<object>(), updater, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbUpdater.UpdateRangeAsync(keys.Cast<object>(), updater, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete(TKey key)
        {
            try
            {
                DbDeleter.Delete<TEntity>(key, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbDeleter.DeleteAsync<TEntity>(key, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void DeleteRange(IEnumerable<TKey> keys)
        {
            try
            {
                DbDeleter.DeleteRange(keys.Cast<object>(), shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbDeleter.DeleteRangeAsync(keys.Cast<object>(), shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}