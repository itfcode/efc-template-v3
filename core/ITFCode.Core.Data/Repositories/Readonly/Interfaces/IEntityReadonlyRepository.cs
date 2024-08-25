using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Data.Repositories.Readonly.Interfaces
{
    public interface IEntityReadonlyRepository<TEntity> where TEntity : class, IEntity
    {

    }
}