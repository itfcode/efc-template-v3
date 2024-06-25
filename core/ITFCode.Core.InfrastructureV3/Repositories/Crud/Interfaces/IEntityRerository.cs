using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces
{
    public interface IEntityRerository<TEntity> where TEntity : class, IEntity
    {
        #region Insert: Sync & Async 

        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        void InsertRange(IEnumerable<TEntity> entities);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Update: Sync & Async 

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Delete: Sync & Async 

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region Save: Sync & Async 

        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}