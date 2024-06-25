using ITFCode.Core.Common.Tests.TestKit;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public abstract class EntityReadonlyBaseRepository_Tests
    {
        #region Private & Protected Fields 

        protected readonly TestDbContext _dbContext;

        #endregion

        #region Constructors

        public EntityReadonlyBaseRepository_Tests()
        {
            _dbContext = DbContextCreator.CreateClear();
        }

        #endregion

        #region Tests

        public abstract void Get_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetAsync_If_Param_Is_Correct_Then_Ok();

        public abstract void GetMany_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetManyAsync_If_Param_Is_Correct_Then_Ok();

        #endregion

        #region Private Methods 

        protected void AddEnities(params object[] entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Add(entity);
            }

            _dbContext.SaveChanges();

            foreach (var entry in _dbContext.ChangeTracker.Entries())
                entry.State = EntityState.Detached;
        }

        #endregion
    }
}