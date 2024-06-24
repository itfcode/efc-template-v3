namespace ITFCode.Core.InfrastructureV3.EfCore.Interfaces
{
    public interface IEfEntityDeleter
    {
        void Delete<TEntity>(TEntity entity, bool shouldSave = false) where TEntity : class;
        Task DeleteAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void Delete<TEntity>(object key, bool shouldSave = false) where TEntity : class;
        Task DeleteAsync<TEntity>(object key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void Delete<TEntity>((object, object) key, bool shouldSave = false) where TEntity : class;
        Task DeleteAsync<TEntity>((object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void Delete<TEntity>((object, object, object) key, bool shouldSave = false) where TEntity : class;
        Task DeleteAsync<TEntity>((object, object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;

        void DeleteRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void DeleteRange<TEntity>(IEnumerable<object> keys, bool shouldSave = false) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(IEnumerable<object> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void DeleteRange<TEntity>(IEnumerable<(object, object)> keys, bool shouldSave = false) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(IEnumerable<(object, object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void DeleteRange<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false) where TEntity : class;
        Task DeleteRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
    }
}