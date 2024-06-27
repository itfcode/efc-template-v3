using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Helper;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly
{
    public abstract class EntityReadonlyRepository<TDbContext, TEntity, TKey1, TKey2> : EntityReadonlyRepository<TDbContext, TEntity>,
        IEntityReadonlyRepository<TEntity, TKey1, TKey2>
        where TEntity : class, IEntity<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TDbContext : DbContext
    {
        #region Constructors

        public EntityReadonlyRepository(TDbContext dbContext) : base(dbContext) { }

        #endregion

        #region IEntityReadonlyRepository Implementation

        public virtual TEntity? Get((TKey1, TKey2) key)
        {
            try
            {
                return DbReader.Get<TEntity>((key.Item1, key.Item2), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity?> GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetAsync<TEntity>((key.Item1, key.Item2), asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }     

        public virtual IReadOnlyCollection<TEntity> GetMany(IEnumerable<(TKey1, TKey2)> keys)
        {
            try
            {
                return DbReader.GetMany<TEntity>(
                    CollectionHelper.ToArraysOfObjects(keys), asNoTracking: true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DbReader.GetManyAsync<TEntity>(
                    CollectionHelper.ToArraysOfObjects(keys), asNoTracking: true, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}