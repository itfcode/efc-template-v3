using ITFCode.Core.InfrastructureV3.EfCore.Base;
using ITFCode.Core.InfrastructureV3.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.EfCore.Readers
{
    public class EfEntityReader<TDbContext> : EfEntityBaseOperator<TDbContext>
        where TDbContext : DbContext
    {
        #region Constructors 

        public EfEntityReader(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region Public Methods: Get Sync & Async 

        public TEntity? Get<TEntity>(object key, bool asNoTracking = true) where TEntity : class
        {
            try
            {
                return Get<TEntity>([key], asNoTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity?> GetAsync<TEntity>(object key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class
        {
            try
            {
                return await GetAsync<TEntity>([key], asNoTracking, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity? Get<TEntity>((object, object) key, bool asNoTracking = true) where TEntity : class
        {
            try
            {
                return Get<TEntity>([key.Item1, key.Item2], asNoTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity?> GetAsync<TEntity>((object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await GetAsync<TEntity>([key.Item1, key.Item2], asNoTracking, cancellationToken);
        }

        public TEntity? Get<TEntity>((object, object, object) key, bool asNoTracking = true) where TEntity : class
        {
            return Get<TEntity>([key.Item1, key.Item2, key.Item3], asNoTracking);
        }

        public async Task<TEntity?> GetAsync<TEntity>((object, object, object) key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await GetAsync<TEntity>([key.Item1, key.Item2, key.Item3], asNoTracking, cancellationToken);
        }

        #endregion

        #region Public Methods: GetMany Sync & Async 

        public IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<object> keys, bool asNoTracking = true) where TEntity : class
        {
            try
            {
                return GetMany(
                        predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()[0]),
                        asNoTracking: asNoTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<object> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await GetManyAsync(
                        predicate: ExpressionHelper.BuildPredicate<TEntity, object>(keys, GetKeyPropertyNames<TEntity>()[0]),
                        asNoTracking: asNoTracking,
                        cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true)
            where TEntity : class
        {
            try
            {
                return GetMany(
                        predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()),
                        asNoTracking: asNoTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<(object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await GetManyAsync(
                        predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()),
                        asNoTracking: asNoTracking,
                        cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IReadOnlyCollection<TEntity> GetMany<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true)
            where TEntity : class
        {
            try
            {
                return GetMany(
                        predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()),
                        asNoTracking: asNoTracking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            try
            {
                return await GetManyAsync(
                        predicate: ExpressionHelper.BuildPredicate<TEntity>(keys, GetKeyPropertyNames<TEntity>()),
                        asNoTracking: asNoTracking,
                        cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Protected Methods 

        protected TEntity? Get<TEntity>(object[] keyValues, bool asNoTracking = true)
            where TEntity : class
        {
            try
            {
                var entity = DbContext.Set<TEntity>().Find(keyValues);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task<TEntity?> GetAsync<TEntity>(object[] keyValues, bool asNoTracking = true, CancellationToken cancellationToken = default)
             where TEntity : class
        {
            try
            {
                var entity = await DbContext.Set<TEntity>().FindAsync(keyValues, cancellationToken);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
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

        protected IReadOnlyCollection<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
             where TEntity : class
        {
            try
            {
                var query = DbContext.Set<TEntity>().Where(predicate);

                if (asNoTracking)
                    query = query.AsNoTracking();

                return [.. query];
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task<IReadOnlyCollection<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
             where TEntity : class
        {
            try
            {
                var query = DbContext.Set<TEntity>().Where(predicate);

                if (asNoTracking)
                    query = query.AsNoTracking();

                return await query.ToListAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity? Get<TEntity>(Expression<Func<TEntity, bool>> predicate)
             where TEntity : class
        {
            try
            {
                return DbContext.Set<TEntity>().FirstOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}