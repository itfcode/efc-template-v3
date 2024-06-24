using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.EfCore;
using ITFCode.Core.InfrastructureV3.EfCore.Readers;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Crud
{
    public abstract class EntityRepository<TDbContext, TEntity> : IEntityRerository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected EfEntityCreater<TDbContext> DbCreater { get; }
        protected EfEntityReader<TDbContext> DbReader { get; }
        protected EfEntityUpdater<TDbContext> DbUpdater { get; }
        protected EfEntityDeleter<TDbContext> DbDeleter { get; }

        #endregion

        #region Constructors

        public EntityRepository(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbCreater = new EfEntityCreater<TDbContext>(dbContext);
            DbReader = new EfEntityReader<TDbContext>(dbContext);
            DbUpdater = new EfEntityUpdater<TDbContext>(dbContext);
            DbDeleter = new EfEntityDeleter<TDbContext>(dbContext);
        }

        #endregion

        #region IEntityRerository Implementation

        public virtual TEntity Insert(TEntity entity, bool shouldSave = false)
        {
            try
            {
                return DbCreater.Insert(entity, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbCreater.InsertAsync(entity, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            try
            {
                DbCreater.InsertRange(entities, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbCreater.InsertRangeAsync(entities, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Update(TEntity entity, bool shouldSave = false)
        {
            try
            {
                return DbUpdater.Update(entity, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbUpdater.UpdateAsync(entity, shouldSave,cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async void UpdateRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual void Delete(TEntity entity, bool shouldSave = false)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}