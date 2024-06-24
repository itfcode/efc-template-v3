using ITFCode.Core.InfrastructureV3.EfCore.Base;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.EfCore
{
    public sealed class EfEntityCreater<TDbContext> : EfEntityBaseOperator<TDbContext>
        where TDbContext : DbContext
    {
        #region Constructors

        public EfEntityCreater(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Public Methods

        public TEntity Insert<TEntity>(TEntity entity, bool shouldSave = false)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                var entityEntry = DbContext.Set<TEntity>().Add(entity);

                if (shouldSave)
                    DbContext.SaveChanges();

                return entityEntry.Entity;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entityEntry = await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

                if (shouldSave)
                    await DbContext.SaveChangesAsync(cancellationToken);

                return entityEntry.Entity;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false)
            where TEntity : class
        {
            CheckCollection(entities);

            try
            {
                DbContext.Set<TEntity>().AddRange(entities);

                if (shouldSave)
                    DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            CheckCollection(entities);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

                if (shouldSave)
                    await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}