using ITFCode.Core.Data.EfOperators;
using ITFCode.Core.Data.Repositories.Readonly.Interfaces;
using ITFCode.Core.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Data.Repositories.Readonly
{
    public abstract class EntityReadonlyRepository<TDbContext, TEntity> : IEntityReadonlyRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        #region Private & Protected Fields

        private EfEntityReader<TDbContext>? _dbReader;

        #endregion

        #region Protected Properties 

        protected TDbContext DbContext { get; }
        protected EfEntityReader<TDbContext> DbReader => _dbReader ??= new EfEntityReader<TDbContext>(DbContext);

        #endregion

        #region Constructors

        public EntityReadonlyRepository(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion
    }
}