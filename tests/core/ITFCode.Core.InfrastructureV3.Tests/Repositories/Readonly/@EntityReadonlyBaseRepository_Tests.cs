using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public abstract class EntityReadonlyBaseRepository_Tests
    {
        #region Private & Protected Fields 

        protected readonly TestDbContext _dbContext;

        #endregion

        #region Protected Properties 

        protected DbSet<UserTc> UserSet => _dbContext.Users;
        protected DbSet<ProductTc> ProductSet => _dbContext.Products;
        protected DbSet<ProductOrderTc> ProductOrderSet => _dbContext.ProductOrders;

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
        public abstract Task GetAsync_Throw_If_Cancellation();

        public abstract void GetMany_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetManyAsync_If_Param_Is_Correct_Then_Ok();
        public abstract Task GetManyAsync_Throw_If_Cancellation();

        #endregion

        #region Private & Protected Methods 

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

        protected async Task AddEnitiesAsync(params object[] entities)
        {
            foreach (var entity in entities)
            {
                await _dbContext.AddAsync(entity);
            }

            await _dbContext.SaveChangesAsync();

            foreach (var entry in _dbContext.ChangeTracker.Entries())
                entry.State = EntityState.Detached;
        }

        protected void AddTestingData()
        {
            AddEnities(
                DefaultData.UserAdmin, DefaultData.UserManager, DefaultData.UserGuest,
                DefaultData.ProductA, DefaultData.ProductB, DefaultData.ProductC,
                DefaultData.ProductOrder1, DefaultData.ProductOrder2, DefaultData.ProductOrder3);
        }

        protected async Task AddTestingDataAsync()
        {
            await AddEnitiesAsync(
                DefaultData.UserAdmin, DefaultData.UserManager, DefaultData.UserGuest,
                DefaultData.ProductA, DefaultData.ProductB, DefaultData.ProductC,
                DefaultData.ProductOrder1, DefaultData.ProductOrder2, DefaultData.ProductOrder3);
        }

        protected CancellationToken CreateCancellationToken(bool throwOnFirstException = true)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel(throwOnFirstException);

            return cancellationToken;
        }

        #endregion
    }
}