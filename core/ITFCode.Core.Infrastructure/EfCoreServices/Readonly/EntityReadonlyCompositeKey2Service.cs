using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Readonly
{
    public class EntityReadonlyService<TDbContext, TEntity, TKey1, TKey2>
        : EntityReadonlyBaseService<TDbContext, TEntity>, IEntityReadonlyService<TEntity, TKey1, TKey2>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        #region Constructors

        public EntityReadonlyService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyService Implenemtation

        public virtual TEntity? Get(TKey1 key1, TKey2 key2, bool asNoTracking = true)
            => base.Get([key1, key2], asNoTracking);

        public virtual TEntity? Get((TKey1, TKey2) key, bool asNoTracking = true)
            => base.Get([key.Item1, key.Item2], asNoTracking);

        public virtual async Task<TEntity?> GetAsync(TKey1 key1, TKey2 key2, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await base.GetAsync([key1, key2], asNoTracking, cancellationToken);

        public virtual async Task<TEntity?> GetAsync((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await base.GetAsync([key.Item1, key.Item2], asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true)
            => GetMany(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey1, TKey2>(keys, GetKeyPropertyNames()),
                asNoTracking: asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await GetManyAsync(
                predicate: ExpressionHelper.BuildPredicate<TEntity, TKey1, TKey2>(keys, GetKeyPropertyNames()),
                asNoTracking: asNoTracking,
                cancellationToken: cancellationToken);

        #endregion

        #region Private & Protected Methods   
        
        #endregion
    }
}