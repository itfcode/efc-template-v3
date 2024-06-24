using ITFCode.Core.Domain.Tests.EfCoreServices.Base;
using ITFCode.Core.Domain.Tests.TestKit;
using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices.Crud;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ITFCode.Core.Domain.Tests.EfCoreServices
{
    public class EntityCrudSingleKeyServiceTests : EntityBaseCrudService_Tests<IEntityCrudService<UserTc, int>>
    {
        #region Constructors 

        public EntityCrudSingleKeyServiceTests() : base() { }

        #endregion

        #region Reading

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
            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.GetAsync(DefaultData.UserUser.Key,
                cancellationToken: cancellationToken));
        }

        #endregion

        #region Creating

        [Fact]
        public override async Task InsertAsync_Throw_If_Cancellation_Requested()
        {
            var cancellationToken = CreateCancellationToken();
            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.InsertAsync(DefaultData.UserUser,
                cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task InsertRangeAsync_Throw_If_Cancellation_Requested()
        {
            var entity = EntityGenerator.CreateUser(99);
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.InsertRangeAsync([entity], cancellationToken: cancellationToken));
        }

        #endregion

        #region Updating

        [Fact]
        public override async Task UpdateAsync_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.UpdateAsync(
                key: entity.Key,
                updater: (x) => { },
                cancellationToken: cancellationToken));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public override async Task UpdateAsync_If_Params_Is_Correct_Then_Ok(bool shouldSave)
        {
            var userId = DefaultData.UserAdmin.Id;

            var userBefore = await _testService.GetAsync(userId, asNoTracking: true);
            Assert.NotNull(userBefore);

            var newLastName = "NewLastName";
            await _testService.UpdateAsync(userId, x => x.LastName = newLastName, true);
            var userAfter = await _testService.GetAsync(userId, asNoTracking: true);
            Assert.NotNull(userAfter);

            Assert.Equal(userBefore.Id, userAfter.Id);

            if (shouldSave)
                Assert.Equal(newLastName, userAfter.LastName);
            else
                Assert.NotEqual(userBefore.LastName, userAfter.LastName);
        }

        [Fact]
        public override async Task UpdateRangeAsync_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.UpdateRangeAsync(
                keys: [entity.Key],
                updater: (x) => { },
                cancellationToken: cancellationToken));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public override async Task UpdateRangeAsync_If_Params_Is_Correct_Then_Ok(bool shouldSave)
        {
            var res = typeof(UserTc)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(KeyAttribute)))
                .Select(p => p.Name)
                .ToArray();

            //_dbContext.Set<UserTc>().EntityType.FindPrimaryKey()
            
            var userAdminId = DefaultData.UserAdmin.Id;
            var userManagerId = DefaultData.UserManager.Id;

            var userAdminBefore = await _testService.GetAsync(userAdminId, asNoTracking: true);
            var userManagerBefore = await _testService.GetAsync(userManagerId, asNoTracking: true);

            Assert.NotNull(userAdminBefore);
            Assert.NotNull(userManagerBefore);

            var newLastName = "NewLastName";
            await _testService.UpdateRangeAsync([userAdminId, userManagerId], x => x.LastName = newLastName, true);
            var userAdminAfter = await _testService.GetAsync(userAdminId, asNoTracking: true);
            var userManagerAfter = await _testService.GetAsync(userManagerId, asNoTracking: true);

            Assert.NotNull(userAdminAfter);
            Assert.NotNull(userManagerAfter);

            Assert.Equal(userAdminBefore.Id, userAdminAfter.Id);
            Assert.Equal(userManagerBefore.Id, userManagerAfter.Id);

            if (shouldSave)
            {
                Assert.Equal(newLastName, userAdminAfter.LastName);
                Assert.Equal(newLastName, userManagerAfter.LastName);
            }
            else
            {
                Assert.NotEqual(userAdminBefore.LastName, userAdminAfter.LastName);
                Assert.NotEqual(userManagerBefore.LastName, userManagerAfter.LastName);
            }
        }

        #endregion

        #region Removing

        [Fact]
        public override async Task DeleteAsync_By_Key_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.DeleteAsync(entity.Key,
                cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task DeleteAsync_By_Entity_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.DeleteAsync(entity.Key, cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Key_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _testService.DeleteRangeAsync([entity.Key], cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Entity_Throw_If_Cancellation_Requested()
        {
            var entity = await _dbContext.Users.FirstAsync();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => _testService.DeleteRangeAsync([entity], cancellationToken: cancellationToken));
        }

        #endregion

        #region Private & Protected Methods 

        protected override IEntityCrudService<UserTc, int> CreateService(TestDbContext dbContext)
            => new EntityCrudService<TestDbContext, UserTc, int>(_dbContext);

        #endregion
    }
}