using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Readonly;
using ITFCode.Core.InfrastructureV2.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Readonly
{
    public class EntityReadonlyService<TDbContext, TEntity, TKey> : EntityReadonlyBaseService<TDbContext, TEntity>,
        IEntityReadonlyService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        #region Constructors

        public EntityReadonlyService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyService<TEntity, TKey>

        public virtual TEntity? Get(TKey key, bool asNoTracking = true)
            => DataReader.Get(key, asNoTracking);

        public virtual async Task<TEntity?> GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DataReader.GetAsync(key, asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
            => DataReader.GetMany(BuildPredicate(keys), asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DataReader.GetManyAsync(BuildPredicate(keys), asNoTracking, cancellationToken);

        #endregion

        #region Protected Methods 

        protected Expression<Func<TEntity, bool>> BuildPredicate(IEnumerable<TKey> keys)
            => ExpressionHelper.BuildPredicate<TEntity, TKey>(keys, GetKeyPropertyNames()[0]);

        #endregion
    }
}