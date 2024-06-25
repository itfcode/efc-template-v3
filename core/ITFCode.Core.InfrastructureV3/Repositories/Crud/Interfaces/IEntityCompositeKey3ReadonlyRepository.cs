using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces
{
    public interface IEntityRepository<TEntity, TKey1, TKey2, TKey3> : IEntityRerository<TEntity>, IEntityReadonlyRepository<TEntity, TKey1, TKey2, TKey3>
        where TEntity : class, IEntity
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
    {
        TEntity Update((TKey1, TKey2, TKey3) key, Action<TEntity> updater);
        Task<TEntity> UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater,  CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater);
        Task UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default);

        void Delete((TKey1, TKey2, TKey3) key);
        Task DeleteAsync((TKey1, TKey2, TKey3) key, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<(TKey1, TKey2, TKey3)> keys);
        Task DeleteRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, CancellationToken cancellationToken = default);
    }
}