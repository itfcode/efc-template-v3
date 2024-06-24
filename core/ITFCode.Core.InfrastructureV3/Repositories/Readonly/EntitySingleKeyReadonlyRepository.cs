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

        public virtual TEntity? Get(TKey key, bool asNoTracking = true)
            => DbReader.Get<TEntity>(key, asNoTracking);

        public virtual async Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DbReader.GetAsync<TEntity>(key, asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
            => DbReader.GetMany<TEntity>(keys.Cast<object>(), asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DbReader.GetManyAsync<TEntity>(keys.Cast<object>(), asNoTracking, cancellationToken);

        #endregion
    }
}