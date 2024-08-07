﻿using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces
{
    public interface IEntityReadonlyRepository<TEntity, TKey1, TKey2>
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        TEntity? Get((TKey1, TKey2) key);
        Task<TEntity?> GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default);

        IReadOnlyCollection<TEntity> GetRange(IEnumerable<(TKey1, TKey2)> keys);
        Task<IReadOnlyCollection<TEntity>> GetRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default);
    }
}