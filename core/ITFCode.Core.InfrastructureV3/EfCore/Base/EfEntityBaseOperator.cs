using ITFCode.Core.Domain.Exceptions.Collections;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.EfCore.Base
{
    public abstract class EfEntityBaseOperator<TDbContext>
        where TDbContext : DbContext
    {
        #region Private & Protected Properties

        protected TDbContext DbContext { get; }

        #endregion

        #region Constructors 

        protected EfEntityBaseOperator(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #endregion

        #region Privater & Protected Method

        protected void CheckCollection<TEntity>(IEnumerable<TEntity> entities) where TEntity : class 
        {
            ArgumentNullException.ThrowIfNull(entities, nameof(entities));

            if (!entities.Any())
                throw new CollectionIsEmptyException();

            if (entities.Any(x => x is null))
                throw new CollectionContainsNullException();
        }

        protected string[] GetKeyPropertyNames<TEntity>() where TEntity : class
        {
            var primaryKey = DbContext.Set<TEntity>().EntityType.FindPrimaryKey()
                ?? throw new Exception("Primary Key Not Found");

            return primaryKey.Properties.Select(x => x.Name).ToArray();
        }

        protected ICollection<object[]> ToArraysOfObjects(IEnumerable<object> values)
        {
            return values.Select(x => new object[] { x }).ToList();
        }

        #endregion
    }
}