﻿using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces
{
    public interface IEntityRepository<TEntity, TKey1, TKey2> : IEntityRerository<TEntity>, IEntityReadonlyRepository<TEntity, TKey1, TKey2>
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        TEntity Update((TKey1, TKey2) key, Action<TEntity> updater, bool shouldSave = false);
        Task<TEntity> UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, bool shouldSave = false);
        Task UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default);

        void Delete((TKey1, TKey2) key, bool shouldSave = false);
        Task DeleteAsync((TKey1, TKey2) key, bool shouldSave = false, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<(TKey1, TKey2)> keys, bool shouldSave = false);
        Task DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, bool shouldSave = false, CancellationToken cancellationToken = default);
    }
}
