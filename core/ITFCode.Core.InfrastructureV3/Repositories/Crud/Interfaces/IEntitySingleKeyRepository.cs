using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces
{
    public interface IEntityRepository<TEntity, TKey> : IEntityRerository<TEntity>, IEntityReadonlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity Update(TKey key, Action<TEntity> updater);
        Task<TEntity> UpdateAsync(TKey key, Action<TEntity> updater, CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater);
        Task UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, CancellationToken cancellationToken = default);

        void Delete(TKey key);
        Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<TKey> keys);
        Task DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
    }
}