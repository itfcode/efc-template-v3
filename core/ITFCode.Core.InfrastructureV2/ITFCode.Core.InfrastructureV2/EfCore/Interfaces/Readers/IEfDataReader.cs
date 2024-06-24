using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCore.Interfaces.Readers
{
    internal interface IEfDataReader<TEntity> where TEntity : class
    {
        #region Get One: Sync & Async 

        TEntity? Get<TKey>(TKey key, bool asNoTracking = true)
            where TKey : IEquatable<TKey>;

        TEntity? Get<TKey1, TKey2>((TKey1, TKey2) key, bool asNoTracking = true)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>;

        TEntity? Get<TKey1, TKey2, TKey3>((TKey1, TKey3, TKey3) key, bool asNoTracking = true)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
            where TKey3 : IEquatable<TKey3>;

        Task<TEntity?> GetAsync<TKey>(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TKey : IEquatable<TKey>;

        Task<TEntity?> GetAsync<TKey1, TKey2>((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>;

        Task<TEntity?> GetAsync<TKey1, TKey2, TKey3>((TKey1, TKey3, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
            where TKey3 : IEquatable<TKey3>;

        #endregion

        #region Get Many: Sync & Async

        #endregion

        IReadOnlyCollection<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);

        Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}