﻿using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Readonly
{
    public interface IEntityReadonlyService<TEntity, TKey1, TKey2, TKey3>
        where TEntity : class, IEntity
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
    {
        TEntity? Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true);
        Task<TEntity?> GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default);

        IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}