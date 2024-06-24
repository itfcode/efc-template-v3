using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces
{
    public interface IEntityReadonlyRepository<TEntity, TKey1, TKey2>
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        TEntity? Get((TKey1, TKey2) key, bool asNoTracking = true);
        Task<TEntity?> GetAsync((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default);

        IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}
