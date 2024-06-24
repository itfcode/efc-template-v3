namespace ITFCode.Core.InfrastructureV3.EfCore.Interfaces
{
    public interface IEfEntityUpdater
    {
        TEntity Update<TEntity>(TEntity entity, bool shouldSave = false) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity Update<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity Update<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity Update<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;

        void UpdateRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false) where TEntity : class;
        Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task UpdateRangeAsync<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task UpdateRangeAsync<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false) where TEntity : class;
        Task UpdateRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
    }
}
