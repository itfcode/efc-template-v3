using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Infrastructure.Repositories.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        #region Sync Methods 

        //Task<TEntity> Get(object id);
        ////TEntity Get(object id);
        //Task<bool> Exist(object id);
        ////TEntity Exist(object id);
        //Task<TEntity> Insert(TEntity entity);
        //Task<TEntity> Update(TEntity entity);
        //Task Delete(object id);

        #endregion

        #region Async Methods

        Task<TEntity?> GetAsync(object id, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(object id, CancellationToken cancellationToken = default);
        //Task<bool> ExistAsync(object id, CancellationToken cancellationToken = default);
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(object id, CancellationToken cancellationToken = default);

        #endregion
    }
}
