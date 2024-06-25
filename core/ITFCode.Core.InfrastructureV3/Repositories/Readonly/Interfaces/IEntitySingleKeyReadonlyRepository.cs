using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces
{
    public interface IEntityReadonlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity? Get(TKey key);
        Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default);

        IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
    }
}