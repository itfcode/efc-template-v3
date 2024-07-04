using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Extensions;
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
            try
            {
                return DbReader.Get<TEntity>((key.Item1, key.Item2), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity?> GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetAsync<TEntity>((key.Item1, key.Item2), asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IReadOnlyCollection<TEntity> GetRange(IEnumerable<(TKey1, TKey2)> keys)
        {
            try
            {
                return DbReader.GetMany<TEntity>(keys.ToTuplesOfObjects(), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetManyAsync<TEntity>(keys.ToTuplesOfObjects(), asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Update((TKey1, TKey2) key, Action<TEntity> updater)
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

        public virtual async Task<TEntity> UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, CancellationToken cancellationToken = default)
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

        public virtual void UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater)
        {
            try
            {
                DbUpdater.UpdateRange(keys.ToTuplesOfObjects(), updater, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbUpdater.UpdateRangeAsync(keys.ToTuplesOfObjects(), updater, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete((TKey1, TKey2) key)
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

        public virtual async Task DeleteAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
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

        public virtual void DeleteRange(IEnumerable<(TKey1, TKey2)> keys)
        {
            try
            {
                DbDeleter.DeleteRange<TEntity>(keys.ToTuplesOfObjects(), shouldSave: false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbDeleter.DeleteRangeAsync<TEntity>(keys.ToTuplesOfObjects(), shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}