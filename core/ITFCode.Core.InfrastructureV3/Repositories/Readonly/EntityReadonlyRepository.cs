using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.EfCore.Readers;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Repositories.Readonly
{
    public abstract class EntityReadonlyRepository<TDbContext, TEntity> : IEntityReadonlyRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected EfEntityReader<TDbContext> DbReader { get; }

        #endregion

        #region Constructors

        public EntityReadonlyRepository(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbReader = new EfEntityReader<TDbContext>(dbContext);
        }

        #endregion
    }
}