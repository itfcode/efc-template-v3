using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly
{
    public abstract class EntityReadonlyRepository<TDbContext, TEntity, TKey> : EntityReadonlyRepository<TDbContext, TEntity>,
        IEntityReadonlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        #region Constructors

        public EntityReadonlyRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyRepository Implementation

        public virtual TEntity? Get(TKey key)
        {
            try
            {
                return DbReader.Get<TEntity>(key, asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetAsync<TEntity>(key, asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys)
        {
            try
            {
                return DbReader.GetMany<TEntity>(keys.Cast<object>(), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetManyAsync<TEntity>(keys.Cast<object>(), asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}