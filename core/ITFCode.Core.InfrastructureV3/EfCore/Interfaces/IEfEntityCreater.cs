namespace ITFCode.Core.InfrastructureV3.EfCore.Interfaces
{
    public interface IEfEntityCreater
    {
        TEntity Insert<TEntity>(TEntity entity, bool shouldSave = false) where TEntity : class;
        Task<TEntity> InsertAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
        void InsertRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false) where TEntity : class;
        Task InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class;
    }
}
