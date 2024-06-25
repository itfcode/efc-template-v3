using ITFCode.Core.Common.Tests.TestKit;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public abstract class EntityBaseRepository_Tests
    {
        #region Private & Protected Fields 

        protected readonly TestDbContext _dbContext;

        #endregion

        #region Constructors

        public EntityBaseRepository_Tests()
        {
            _dbContext = DbContextCreator.CreateClear();
        }

        #endregion

        #region Tests: Insert(TEntity entity)

        #endregion

        #region Tests: InsertAsync(TEntity entity, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: InsertRange(IEnumerable<TEntity> entities)

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Update(TEntity entity)

        #endregion

        #region Tests: UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: UpdateRange(IEnumerable<TEntity> entities)

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Delete(TEntity entity)

        #endregion

        #region Tests: DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: DeleteRange(IEnumerable<TEntity> entities)

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        #endregion
    }
}