using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces
{
    public interface IEntityReadonlyService<TEntity>
        where TEntity : class, IEntity
    {
    }
}