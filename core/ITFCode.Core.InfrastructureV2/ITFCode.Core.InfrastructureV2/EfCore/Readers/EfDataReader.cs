using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV2.EfCore.Interfaces.Readers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCore.Readers
{
    internal class EfDataReader<TDbContext, TEntity> : IEfDataReader<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity
    {
        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        #endregion

        #region Constructors 

        public EfDataReader(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        #endregion

        #region IEfDbReader<TEntity, TKey> Implementation

        public TEntity? Get<TKey>(TKey key, bool asNoTracking = true) where TKey : IEquatable<TKey>
        {
            return Get([key], asNoTracking);
        }

        public TEntity? Get<TKey1, TKey2>((TKey1, TKey2) key, bool asNoTracking = true)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
        {
            return Get([key.Item1, key.Item2], asNoTracking);
        }

        public TEntity? Get<TKey1, TKey2, TKey3>((TKey1, TKey3, TKey3) key, bool asNoTracking = true)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
            where TKey3 : IEquatable<TKey3>
        {
            return Get([key.Item1, key.Item2, key.Item3], asNoTracking);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            try
            {
                var entity = DbSet.FirstOrDefault(predicate);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

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

        public IReadOnlyCollection<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            var query = DbSet.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            var entities = query.ToList();

            if (entities.Count > 0 && asNoTracking)
            {
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Detached;
                }
            }
            return entities;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            var query = DbSet.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            var entities = await query.ToListAsync(cancellationToken);

            if (entities.Count > 0 && asNoTracking)
            {
                foreach (var entity in entities)
                {
                    DbContext.Entry(entity).State = EntityState.Detached;
                }
            }

            return entities;
        }

        public Task<TEntity?> GetAsync<TKey>(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default) where TKey : IEquatable<TKey>
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetAsync<TKey1, TKey2>((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetAsync<TKey1, TKey2, TKey3>((TKey1, TKey3, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            where TKey1 : IEquatable<TKey1>
            where TKey2 : IEquatable<TKey2>
            where TKey3 : IEquatable<TKey3>
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private & Protected Methods

        protected TEntity? Get(object[] keyValues, bool asNoTracking = true)
        {
            try
            {
                var entity = DbSet.Find(keyValues);

                if (entity is not null && asNoTracking)
                    DbContext.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task<TEntity?> GetAsync(object[] keyValues, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await DbSet.FindAsync(keyValues, cancellationToken);

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

        #endregion

    }
}
