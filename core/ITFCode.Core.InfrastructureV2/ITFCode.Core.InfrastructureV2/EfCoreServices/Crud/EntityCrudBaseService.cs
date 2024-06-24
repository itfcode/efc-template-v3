using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Crud;
using ITFCode.Core.InfrastructureV2.EfCoreServices.Readonly;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Crud
{
    public abstract class EntityCrudBaseService<TDbContext, TEntity> : EntityReadonlyBaseService<TDbContext, TEntity>,
        IEntityCrudService<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        #region Constructors 

        protected EntityCrudBaseService(TDbContext dbContext) : base(dbContext) 
        {
        }

        #endregion

        #region IEntityCrudService<TEntity> Implementation

        public void Delete(TEntity entity, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity Insert(TEntity entity, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> InsertAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task InsertRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}