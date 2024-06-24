using ITFCode.Core.Domain.Tests.EfCoreServices.Base;
using ITFCode.Core.Domain.Tests.TestKit;
using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Readonly;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.EfCoreServices
{
    public class EntityReadonlyCompositeKey2Service_Tests : EntityBaseReadonlyService_Tests<IEntityReadonlyService<UserRoleTc, int, int>>
    {
        #region Tests

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var dbContext = DbContextCreator.CreateFulled();
            var userRoleService = new EntityReadonlyService<TestDbContext, UserRoleTc, int, int>(dbContext);

            var userId = DefaultData.UserAdmin.Id;
            var roleId = DefaultData.RoleAdmin.Id;

            var userRole = userRoleService.Get(userId, roleId, asNoTracking);
            Assert.NotNull(userRole);
            Assert.Equal(userId, userRole.Key1);
            Assert.Equal(roleId, userRole.Key2);
            Assert.Equal(entityState, dbContext.Entry(userRole).State);

            userRole = userRoleService.Get((userId, roleId), asNoTracking);
            Assert.NotNull(userRole);
            Assert.Equal(userId, userRole.Key1);
            Assert.Equal(roleId, userRole.Key2);
            Assert.Equal(entityState, dbContext.Entry(userRole).State);
        }

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override async Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var dbContext = DbContextCreator.CreateFulled();
            var userRoleService = new EntityReadonlyService<TestDbContext, UserRoleTc, int, int>(dbContext);

            var userId = DefaultData.UserAdmin.Id;
            var roleId = DefaultData.RoleAdmin.Id;

            var userRole = await userRoleService.GetAsync(userId, roleId, asNoTracking);
            Assert.NotNull(userRole);
            Assert.Equal(userId, userRole.Key1);
            Assert.Equal(roleId, userRole.Key2);
            Assert.Equal(entityState, dbContext.Entry(userRole).State);

            userRole = await userRoleService.GetAsync((userId, roleId), asNoTracking);
            Assert.NotNull(userRole);
            Assert.Equal(userId, userRole.Key1);
            Assert.Equal(roleId, userRole.Key2);
            Assert.Equal(entityState, dbContext.Entry(userRole).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation_Requested()
        {
            var dbContext = DbContextCreator.CreateFulled();
            var userRoleService = new EntityReadonlyService<TestDbContext, UserRoleTc, int, int>(dbContext);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel(true);

            var userId = DefaultData.UserAdmin.Id;
            var roleId = DefaultData.RoleAdmin.Id;

            await Assert.ThrowsAsync<OperationCanceledException>(() => userRoleService.GetAsync(userId, roleId, cancellationToken: cancellationToken));
            await Assert.ThrowsAsync<OperationCanceledException>(() => userRoleService.GetAsync((userId, roleId), cancellationToken: cancellationToken));
        }

        #endregion

        #region Private & Protected Methods 

        protected override IEntityReadonlyService<UserRoleTc, int, int> CreateService(TestDbContext dbContext)
            => new EntityReadonlyService<TestDbContext, UserRoleTc, int, int>(dbContext);

        public override void GetMany_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public override Task GetManyAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public override Task GetManyAsync_Throw_If_Cancellation_Requested()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
