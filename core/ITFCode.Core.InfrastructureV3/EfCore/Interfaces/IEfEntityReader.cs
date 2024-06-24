namespace ITFCode.Core.InfrastructureV3.EfCore.Interfaces
{
    public interface IEfEntityReader
    {
        TEntity? Get<TEntity>(object key, bool asNoTracking = true) where TEntity : class;
        Task<TEntity?> GetAsync<TEntity>(object key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity? Get<TEntity>((object, object) key, bool asNoTracking = true) where TEntity : class;
        Task<TEntity?> GetAsync<TEntity>((object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity? Get<TEntity>((object, object, object) key, bool asNoTracking = true) where TEntity : class;
        Task<TEntity?> GetAsync<TEntity>((object, object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;

        IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<object> keys, bool asNoTracking = true) where TEntity : class;
        Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<object> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;
        IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true) where TEntity : class;
        Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;
        IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true) where TEntity : class;
        Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class;
    }
}