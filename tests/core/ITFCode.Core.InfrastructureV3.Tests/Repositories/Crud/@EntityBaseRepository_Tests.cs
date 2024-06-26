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

        #region Tests: Get, GetAsync, GetMany & GetManyAsync

        public abstract void Get_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetAsync_If_Param_Is_Correct_Then_Ok();

        public abstract void GetMany_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetManyAsync_If_Param_Is_Correct_Then_Ok();

        #endregion

        #region Tests: Insert, InsertAsync, InsertRange & InsertRangeAsync

        public abstract void Insert_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertAsync_If_Param_Is_Correct_Then_Ok();

        public abstract void InsertRange_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok();

        #endregion

        #region Tests: Update, UpdateAsync, UpdateMany & UpdateManyAsync

        public abstract void Update_If_Param_Is_Correct_Then_Ok();
        public abstract Task UpdateAsync_If_Param_Is_Correct_Then_Ok();

        public abstract void UpdateRange_If_Param_Is_Correct_Then_Ok();
        public abstract Task UpdateRangeAsync_If_Param_Is_Correct_Then_Ok();

        #endregion

        #region Tests: Delete, DeleteAsync, DeleteMany & DeleteManyAsync

        public abstract void Delete_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteAsync_If_Param_Is_Correct_Then_Ok();

        public abstract void DeleteRange_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteRangeAsync_If_Param_Is_Correct_Then_Ok();

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