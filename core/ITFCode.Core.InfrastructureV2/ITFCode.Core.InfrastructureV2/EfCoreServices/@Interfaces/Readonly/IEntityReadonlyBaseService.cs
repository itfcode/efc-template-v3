using ITFCode.Core.Domain.Entities.Base.Interfaces;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV2.EfCoreServices.Interfaces.Readonly
{
    public interface IEntityReadonlyService<TEntity>
        where TEntity : class, IEntity
    {
        TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}