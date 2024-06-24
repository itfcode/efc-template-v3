using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces
{
    public interface IEntityCrudService<TEntity> : IEntityReadonlyService<TEntity>
        where TEntity : class, IEntity
    {
        #region Insert: Sync & Async 

        TEntity Insert(TEntity entity, bool shouldSave = false);
        Task<TEntity> InsertAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default);

        void InsertRange(IEnumerable<TEntity> entities, bool shouldSave = false);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default);

        #endregion

        #region Update: Sync & Async 

        TEntity Update(TEntity entity, bool shouldSave = false);
        Task<TEntity> UpdateAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default);

        void UpdateRange(IEnumerable<TEntity> entities, bool shouldSave = false);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default);

        #endregion

        #region Delete: Sync & Async 

        void Delete(TEntity entity, bool shouldSave = false);
        Task DeleteAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default);

        void DeleteRange(IEnumerable<TEntity> entities, bool shouldSave = false);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default);

        #endregion
    }
}