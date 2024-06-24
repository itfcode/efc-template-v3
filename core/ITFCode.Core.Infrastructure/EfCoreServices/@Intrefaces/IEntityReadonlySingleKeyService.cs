using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces
{
    public interface IEntityReadonlyService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntity? Get(TKey key, bool asNoTracking = true);

        Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default);

        IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}