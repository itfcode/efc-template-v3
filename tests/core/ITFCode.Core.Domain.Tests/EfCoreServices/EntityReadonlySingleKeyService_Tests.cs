using ITFCode.Core.Domain.Tests.EfCoreServices.Base;
using ITFCode.Core.Domain.Tests.TestKit;
using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Readonly;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.EfCoreServices
{
    public class EntityReadonlySingleKeyService_Tests : EntityBaseReadonlyService_Tests<IEntityReadonlyService<UserTc, int>>
    {
        #region Constructors

        public EntityReadonlySingleKeyService_Tests() { }

        #endregion

        #region Tests: Get

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var user = _testService.Get(1, asNoTracking);
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
            Assert.Equal(entityState, _dbContext.Entry(user).State);
        }

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override async Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var user = await _testService.GetAsync(1, asNoTracking);
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
            Assert.Equal(entityState, _dbContext.Entry(user).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation_Requested()
        {
            var cancellationToken = CreateCancellationToken();
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _testService.GetAsync(1, cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation_Requested()
        {
            var cancellationToken = CreateCancellationToken();
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _testService.GetManyAsync([1], cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: GetMany

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override void GetMany_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var userIds = _dbContext.Users.Select(u => u.Id).Take(2).ToList();

            var users = _testService.GetMany([.. userIds], asNoTracking);

            Assert.NotEmpty(users);
            Assert.Equal(userIds.Count, users.Count);
            foreach (var user in users)
            {
                Assert.Contains(user.Key, userIds);
                Assert.Equal(entityState, _dbContext.Entry(user).State);
            }
        }

        #endregion

        #region Private Methods 

        protected override IEntityReadonlyService<UserTc, int> CreateService(TestDbContext dbContext)
            => new EntityReadonlyService<TestDbContext, UserTc, int>(dbContext);

        public override Task GetManyAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}