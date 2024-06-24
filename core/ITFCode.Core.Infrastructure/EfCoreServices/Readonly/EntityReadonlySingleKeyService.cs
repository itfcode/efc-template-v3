using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Readonly
{
    public class EntityReadonlyService<TDbContext, TEntity, TKey>
        : EntityReadonlyBaseService<TDbContext, TEntity>, IEntityReadonlyService<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        #region Constructors

        public EntityReadonlyService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyService Implenemtation

        public virtual TEntity? Get(TKey key, bool asNoTracking = true)
            => base.Get([key], asNoTracking);

        public virtual async Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await base.GetAsync([key], asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
            => GetMany(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey>(keys, GetKeyPropertyNames()[0]),
                asNoTracking: asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await GetManyAsync(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey>(keys, GetKeyPropertyNames()[0]),
                asNoTracking: asNoTracking,
                cancellationToken: cancellationToken);

        #endregion

        #region Private & Protected Methods 
        #endregion
    }
}