using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Readonly;
using ITFCode.Core.InfrastructureV2.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Readonly
{
    public class EntityReadonlyService<TDbContext, TEntity, TKey1, TKey2> : EntityReadonlyBaseService<TDbContext, TEntity>,
        IEntityReadonlyService<TEntity, TKey1, TKey2>
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>

        where TDbContext : DbContext
    {
        #region Constructors

        public EntityReadonlyService(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyService<TEntity, TKey1, TKey2>

        public virtual TEntity? Get((TKey1, TKey2) key, bool asNoTracking = true)
            => DataReader.Get(key, asNoTracking);

        public virtual async Task<TEntity?> GetAsync((TKey1, TKey2) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DataReader.GetAsync(key, asNoTracking, cancellationToken);

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true)
            => DataReader.GetMany(BuildPredicate(keys), asNoTracking);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
            => await DataReader.GetManyAsync(BuildPredicate(keys), asNoTracking, cancellationToken);

        #endregion

        #region Protected Methods 

        protected Expression<Func<TEntity, bool>> BuildPredicate(IEnumerable<(TKey1, TKey2)> keys)
            => ExpressionHelper.BuildPredicate<TEntity, TKey1, TKey2>(keys, GetKeyPropertyNames());

        #endregion
    }
}