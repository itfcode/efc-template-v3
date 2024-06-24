using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Helper;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly
{
    public abstract class EntityReadonlyRepository<TDbContext, TEntity, TKey1, TKey2, TKey3> : EntityReadonlyRepository<TDbContext, TEntity>,
        IEntityReadonlyRepository<TEntity, TKey1, TKey2, TKey3>
        where TEntity : class, IEntity<TKey1, TKey2, TKey3>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
        where TDbContext : DbContext
    {
        #region Constructors

        public EntityReadonlyRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyRepository Implementation

        public virtual TEntity? Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)
            => DbReader.Get<TEntity>((key.Item1, key.Item2, key.Item3), asNoTracking);

        public virtual async Task<TEntity?> GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DbReader.GetAsync<TEntity>((key.Item1, key.Item2, key.Item3), asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true)
        {
            return DbReader.GetMany<TEntity>(CollectionHelper.ToArraysOfObjects(keys), asNoTracking);
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            return await DbReader.GetManyAsync<TEntity>(CollectionHelper.ToArraysOfObjects(keys), asNoTracking, cancellationToken);
        }

        #endregion
    }
}