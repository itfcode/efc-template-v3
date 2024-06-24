using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.EfCore
{
    public abstract class EfEntityBaseOperator_Tests
    {
        #region Private & Protected Fields 

        protected readonly TestDbContext _dbContext;

        #endregion

        #region Protected Properties

        protected DbSet<UserTc> UserSet => _dbContext.Users;
        protected DbSet<RoleTc> RoleSet => _dbContext.Roles;
        protected DbSet<UserRoleTc> UserRoleSet => _dbContext.UserRoles;
        protected DbSet<ProductTc> ProductSet => _dbContext.Products;
        protected DbSet<ProductOrderTc> ProductOrderSet => _dbContext.ProductOrders;

        #endregion

        #region Constructors

        public EfEntityBaseOperator_Tests()
        {
            _dbContext = DbContextCreator.CreateClear();
        }

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