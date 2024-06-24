using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Domain.Exceptions.Collections;
using ITFCode.Core.Domain.Exceptions.DbContext;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Readonly;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Crud
{
    public abstract class EntityCrudBaseService<TDbContext, TEntity>
        : EntityReadonlyBaseService<TDbContext, TEntity>, IEntityCrudService<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        #region Constructors

        public EntityCrudBaseService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityCrudService.Inserting: Sync & Async

        public virtual TEntity Insert(TEntity entity, bool shouldSave = false)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                var entityEntry = DbSet.Add(entity);

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

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entityEntry = await DbSet.AddAsync(entity, cancellationToken);

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

        public virtual void InsertRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            CheckCollection(entities);

            try
            {
                DbSet.AddRange(entities);

                if (shouldSave)
                    DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            CheckCollection(entities);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await DbSet.AddRangeAsync(entities, cancellationToken);

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

        #region IEntityCrudService.Updating: Sync & Async

        public virtual TEntity Update(TEntity entity, bool shouldSave = false)
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

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

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

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            CheckCollection(entities);

            throw new NotImplementedException();
        }

        #endregion

        #region IEntityCrudService.Deleting: Sync & Async

        public virtual void Delete(TEntity entity, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        public virtual async Task DeleteAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var state = DbContext.Entry(entity).State;
                DbSet.Attach(entity);
                DbSet.Remove(entity);

                if (shouldSave)
                    await CommitAsync(cancellationToken);
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
                DbSet.AttachRange(entities);
                DbSet.RemoveRange(entities);

                if (shouldSave)
                    Commit();
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
                cancellationToken.ThrowIfCancellationRequested();

                DbSet.AttachRange(entities);
                DbSet.RemoveRange(entities);

                if (shouldSave)
                    await CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods 

        protected virtual TEntity Update(object[] key, Action<TEntity> updater, bool shouldSave = false)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0)
                throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            var entity = Get(key, false)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                updater(entity);

                if (shouldSave)
                    DbContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        protected virtual async Task<TEntity> UpdateAsync(object key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync([key], updater, shouldSave, cancellationToken);
        }

        protected virtual async Task<TEntity> UpdateAsync(object[] key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            cancellationToken.ThrowIfCancellationRequested();

            var entity = await GetAsync(key, false, cancellationToken)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                updater(entity);

                if (shouldSave)
                    await CommitAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message, ex);
            }
        }

        protected virtual void UpdateRange(IEnumerable<object[]> keys, Action<TEntity> updater, bool shouldSave = false)
        {
            if (!keys.Any()) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            foreach (var key in keys)
            {
                var entity = Get(key, false)
                    ?? throw new EntityNotFoundException(key, typeof(TEntity));

                updater(entity);
            }

            if (shouldSave) Commit();
        }

        protected virtual async Task UpdateRangeAsync(IEnumerable<object[]> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            if (!keys.Any()) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            ArgumentNullException.ThrowIfNull(updater, nameof(updater));

            cancellationToken.ThrowIfCancellationRequested();

            foreach (var key in keys)
            {
                var entity = await GetAsync(key, false, cancellationToken)
                    ?? throw new EntityNotFoundException(key, typeof(TEntity));

                updater(entity);
            }

            if (shouldSave) await CommitAsync(cancellationToken);
        }

        protected virtual void Delete(object[] key, bool shouldSave = false)
        {
            var entity = DbSet.Find(key) ?? throw new EntityNotFoundException(key, typeof(TEntity));

            DbSet.Entry(entity).State = EntityState.Deleted;

            if (shouldSave) Commit();
        }

        protected virtual async Task DeleteAsync(object[] key, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var entity = await GetAsync(key, cancellationToken: cancellationToken) ?? throw new EntityNotFoundException(key, typeof(TEntity));

                DbSet.Entry(entity).State = EntityState.Deleted;

                if (shouldSave) await CommitAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected virtual void DeleteRange(IEnumerable<object> keys, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        protected virtual void DeleteRange(IEnumerable<object[]> keys, bool shouldSave = false)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task DeleteRangeAsync(object[] keys, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task DeleteRangeAsync(IEnumerable<object[]> keys, bool shouldSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                //DbSet.AttachRange(entities);
                //DbSet.RemoveRange(entities);

                if (shouldSave)
                    await CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual int Commit()
        {
            return DbContext.SaveChanges();
        }

        protected virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        protected void CheckCollection(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));

            if (!entities.Any())
                throw new CollectionIsEmptyException();

            if (entities.Any(x => x is null))
                throw new CollectionContainsNullException();
        }

        protected ICollection<object> ToObjects<T>(IEnumerable<T> values) where T : IEquatable<T>
        {
            return values.Select(x => (object)x).ToList();
        }

        /// <summary>
        /// Converts collection of values to collection of arrays of values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">collection of values</param>
        /// <returns>collection of arrays of objects</returns>
        protected ICollection<object[]> ToArraysOfObjects<T>(IEnumerable<T> values) where T : IEquatable<T>
        {
            return values.Select(x => new object[] { x }).ToList();
        }
        protected ICollection<object[]> ToArraysOfObjects<T1, T2>(IEnumerable<(T1, T2)> values)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
        {
            return values.Select(x => new object[] { x.Item1, x.Item2 }).ToList();
        }
        protected ICollection<object[]> ToArraysOfObjects<T1, T2, T3>(IEnumerable<(T1, T2, T3)> values)
            where T1 : IEquatable<T1>
            where T2 : IEquatable<T2>
            where T3 : IEquatable<T3>
        {
            return values.Select(x => new object[] { x.Item1, x.Item2, x.Item3 }).ToList();
        }

        #endregion
    }
}