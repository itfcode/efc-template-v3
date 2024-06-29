using ITFCode.Core.Domain.Exceptions.Collections;
using ITFCode.Core.Domain.Exceptions.DbContext;
using ITFCode.Core.InfrastructureV3.EfCore.Base;
using ITFCode.Core.InfrastructureV3.Helper;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.EfCore
{
    public class EfEntityUpdater<TDbContext> : EfEntityBaseOperator<TDbContext>
        where TDbContext : DbContext
    {
        #region Constructors

        public EfEntityUpdater(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Update One : Sync & Async

        public TEntity Update<TEntity>(TEntity entity, bool shouldSave = false)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;

                if (shouldSave)
                    DbContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;

                if (shouldSave)
                    await DbContext.SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        public TEntity Update<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                return Update([key], updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await UpdateAsync([key], updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Update<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                return Update([key.Item1, key.Item2], updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await UpdateAsync([key.Item1, key.Item2], updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Update<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                return Update([key.Item1, key.Item2, key.Item3], updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await UpdateAsync([key.Item1, key.Item2, key.Item3], updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Update Many: Sync & Async

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false)
            where TEntity : class
        {
            CheckCollection(entities);

            try
            {
                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Modified;

                if (shouldSave) 
                    DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new UpdateRangeEntityException(ex.Message, ex);
            }
        }

        public async Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            CheckCollection(entities);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Modified;

                if (shouldSave) await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new UpdateRangeEntityException(ex.Message, ex);
            }
        }

        public void UpdateRange<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                UpdateRange(ToArraysOfObjects(keys), updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRangeAsync<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                await UpdateRangeAsync(ToArraysOfObjects(keys), updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateRange<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                UpdateRange(CollectionHelper.ToArraysOfObjects(keys), updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRangeAsync<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                await UpdateRangeAsync(CollectionHelper.ToArraysOfObjects(keys), updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateRange<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                UpdateRange(CollectionHelper.ToArraysOfObjects(keys), updater, shouldSave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                await UpdateRangeAsync(CollectionHelper.ToArraysOfObjects(keys), updater, shouldSave, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private Methods 

        protected TEntity Update<TEntity>(object[] key, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            var entity = DbContext.Set<TEntity>().Find(key)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                updater(entity);

                //DbContext.Entry(entity).State = EntityState.Modified;

                if (shouldSave)
                    DbContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        protected async Task<TEntity> UpdateAsync<TEntity>(object[] key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            cancellationToken.ThrowIfCancellationRequested();

            await DbContext.Set<TEntity>().FindAsync(key, cancellationToken).ConfigureAwait(false);

            var entity = await DbContext.Set<TEntity>().FindAsync(key, cancellationToken)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                updater(entity);

                if (shouldSave)
                    await DbContext.SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        protected void UpdateRange<TEntity>(IEnumerable<object[]> keys, Action<TEntity> updater, bool shouldSave = false)
            where TEntity : class
        {
            if (!keys.Any()) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            foreach (var key in keys)
            {
                var entity = DbContext.Set<TEntity>().Find(key)
                    ?? throw new EntityNotFoundException(key, typeof(TEntity));

                updater(entity);
            }

            if (shouldSave)
                DbContext.SaveChanges();
        }

        protected async Task UpdateRangeAsync<TEntity>(IEnumerable<object[]> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            if (!keys.Any()) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            cancellationToken.ThrowIfCancellationRequested();

            foreach (var key in keys)
            {
                var entity = await DbContext.Set<TEntity>().FindAsync(key, cancellationToken)
                    ?? throw new EntityNotFoundException(key, typeof(TEntity));

                updater(entity);
            }

            if (shouldSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}