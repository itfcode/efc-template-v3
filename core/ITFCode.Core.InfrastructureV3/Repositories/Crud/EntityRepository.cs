using ITFCode.Core.Domain.Entities.Base;
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

        public virtual TEntity Insert(TEntity entity)
        {
            try
            {
                return DbCreater.Insert(entity, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbCreater.InsertAsync(entity, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            try
            {
                DbCreater.InsertRange(entities, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbCreater.InsertRangeAsync(entities, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                return DbUpdater.Update(entity, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbUpdater.UpdateAsync(entity, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                DbUpdater.UpdateRange(entities, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbUpdater.UpdateRangeAsync(entities, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Delete(TEntity entity)
        {
            try
            {
                DbDeleter.Delete(entity, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbDeleter.DeleteAsync(entity, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                DbDeleter.DeleteRange(entities, shouldSave: false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await DbDeleter.DeleteRangeAsync(entities, shouldSave: false, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Commit()
        {
            try
            {
                var numberOf = DbContext.SaveChanges();

                foreach (var entry in DbContext.ChangeTracker.Entries())
                    entry.State = EntityState.Detached;

                return numberOf;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var numberOf = await DbContext.SaveChangesAsync(cancellationToken);

                foreach (var entry in DbContext.ChangeTracker.Entries())
                    entry.State = EntityState.Detached;

                return numberOf;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}