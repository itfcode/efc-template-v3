using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces
{
    public interface IEntityCrudService<TEntity, TKey> : IEntityCrudService<TEntity>, IEntityReadonlyService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Update(TKey key, Action<TEntity> updater, bool shouldSave = false);
        Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false);
        Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default);

        void Delete(TKey key, bool shouldSave = false);
        Task DeleteAsync(TKey key, bool shouldSave = false, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<TKey> keys, bool shouldSave = false);
        Task DeleteRangeAsync(IEnumerable<TKey> keys, bool shouldSave = false, CancellationToken cancellationToken = default);
    }
}