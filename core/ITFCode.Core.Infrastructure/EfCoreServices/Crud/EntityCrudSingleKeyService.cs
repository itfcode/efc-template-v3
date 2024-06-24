using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Crud
{
    public class EntityCrudService<TDbContext, TEntity, TKey>
        : EntityCrudBaseService<TDbContext, TEntity>, IEntityCrudService<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Constructors

        public EntityCrudService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityCrudService.Reading: Sync & Async  

        public virtual TEntity? Get(TKey key, bool asNoTracking = true)
            => base.Get([key], asNoTracking);

        public virtual async Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await base.GetAsync([key], asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
            => GetMany(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey>(keys, GetKeyPropertyNames()[0]),
                asNoTracking: asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await GetManyAsync(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey>(keys, GetKeyPropertyNames()[0]),
                asNoTracking: asNoTracking,
                cancellationToken: cancellationToken);

        #endregion

        #region IEntityCrudService.Updating: Sync & Async  

        public virtual TEntity Update(TKey key, Action<TEntity> updater, bool shouldSave = false)
            => base.Update([key], updater, shouldSave);

        public virtual async Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            => await base.UpdateAsync([key], updater, shouldSave, cancellationToken);

        public virtual void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false)
            => UpdateRange(ToArraysOfObjects(keys), updater, shouldSave);

        public virtual async Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            => await UpdateRangeAsync(ToArraysOfObjects(keys), updater, shouldSave, cancellationToken);

        #endregion

        #region IEntityCrudService.Deleting: Sync & Async

        public virtual void Delete(TKey key, bool shouldSave = false)
            => base.Delete([key], shouldSave);

        public virtual async Task DeleteAsync(TKey key, bool shouldSave = false, CancellationToken cancellationToken = default)
            => await base.DeleteAsync([key], shouldSave, cancellationToken);

        public virtual void DeleteRange(IEnumerable<TKey> keys, bool shouldSave = false)
            => base.DeleteRange(ToArraysOfObjects(keys), shouldSave);

        public virtual async Task DeleteRangeAsync(IEnumerable<TKey> keys, bool shouldSave = false, CancellationToken cancellationToken = default)
            => await base.DeleteRangeAsync(ToArraysOfObjects(keys), shouldSave, cancellationToken);

        #endregion

        #region Protected Methods 
        #endregion
    }
}