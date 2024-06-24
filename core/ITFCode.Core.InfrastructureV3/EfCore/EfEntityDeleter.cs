using ITFCode.Core.Domain.Entities.Base;
using ITFCode.Core.Domain.Exceptions.Collections;
using ITFCode.Core.Domain.Exceptions.DbContext;
using ITFCode.Core.InfrastructureV3.EfCore.Base;
using ITFCode.Core.InfrastructureV3.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.EfCore
{
    public sealed class EfEntityDeleter<TDbContext> : EfEntityBaseOperator<TDbContext>
        where TDbContext : DbContext
    {
        #region Constructors 

        public EfEntityDeleter(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Public Methods : Delete One - Sync & Async 

        public void Delete<TEntity>(TEntity entity, bool shouldSave = false) where TEntity : class
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        public void Delete<TEntity>(object key, bool shouldSave = false) where TEntity : class
            => Delete<TEntity>([key], shouldSave);

        public async Task DeleteAsync<TEntity>(object key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
            => await DeleteAsync<TEntity>([key], shouldSave);

        public void Delete<TEntity>((object, object) key, bool shouldSave = false) where TEntity : class
            => Delete<TEntity>([key.Item1, key.Item2], shouldSave);

        public async Task DeleteAsync<TEntity>((object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
            => await DeleteAsync<TEntity>([key.Item1, key.Item2], shouldSave, cancellationToken);

        public void Delete<TEntity>((object, object, object) key, bool shouldSave = false) where TEntity : class
            => Delete<TEntity>([key.Item1, key.Item2, key.Item3], shouldSave);

        public async Task DeleteAsync<TEntity>((object, object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
            => await DeleteAsync<TEntity>([key.Item1, key.Item2, key.Item3], shouldSave, cancellationToken);

        #endregion

        #region Public Methods: Delete Many - Sync & Async 

        public void DeleteRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false) where TEntity : class
        {
            try
            {
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Deleted;
                }

                if (shouldSave)
                    DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        public async Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
        {
            try
            {
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Deleted;
                }

                if (shouldSave)
                    await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        public void DeleteRange<TEntity>(IEnumerable<object> keys, bool shouldSave = false) where TEntity : class
        {
            DeleteRange(
                predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()[0]),
                shouldSave: shouldSave);
        }

        public async Task DeleteRangeAsync<TEntity>(IEnumerable<object> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
        {
            await DeleteRangeAsync(
                predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()[0]),
                shouldSave: shouldSave,
                cancellationToken: cancellationToken);
        }

        public void DeleteRange<TEntity>(IEnumerable<(object, object)> keys, bool shouldSave = false) where TEntity : class
        {
        }

        public async Task DeleteRangeAsync<TEntity>(IEnumerable<(object, object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void DeleteRange<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false) where TEntity : class
        {
        }

        public async Task DeleteRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default) where TEntity : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods 

        private void Delete<TEntity>(object[] key, bool shouldSave = false)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            var entity = DbContext.Set<TEntity>().Find(key)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    DbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        private async Task DeleteAsync<TEntity>(object[] key, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            if (key.Length == 0) throw new CollectionIsEmptyException($"Key Is Not Defined.");

            var entity = await DbContext.Set<TEntity>().FindAsync(key, cancellationToken)
                ?? throw new NullReferenceException($"Entity Not Found. Type: '{typeof(TEntity).Name}', Key:'{key}' ");

            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        private void DeleteRange<TEntity>(Expression<Func<TEntity, bool>> predicate, bool shouldSave = false)
            where TEntity : class
        {
            try
            {
                var entities = DbContext.Set<TEntity>()
                    .Where(predicate)
                    .ToList();

                if (entities.Count == 0)
                    return;

                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        private async Task DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool shouldSave = false, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                var entities = await DbContext.Set<TEntity>()
                    .Where(predicate)
                    .ToListAsync(cancellationToken);

                if (entities.Count == 0)
                    return;

                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Deleted;

                if (shouldSave)
                    await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new DeleteEntityException(ex.Message, ex);
            }
        }

        #endregion
    }
}