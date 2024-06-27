using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntitySingleKeyReadonlyRepository_Test : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get(TKey key, bool asNoTracking = true)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var userId = DefaultData.UserAdmin.Id;
            var repository = CreateRepository();
            var entity = repository.Get(userId);

            Assert.NotNull(entity);
            Assert.Equal(userId, entity.Key);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        #endregion

        #region Tests: GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var userId = DefaultData.UserAdmin.Id;
            var repository = CreateRepository();
            var enity = await repository.GetAsync(userId);

            Assert.NotNull(enity);
            Assert.Equal(userId, enity.Key);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(enity).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync(DefaultData.UserAdmin.Key, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var userId1 = DefaultData.UserAdmin.Id;
            var userId2 = DefaultData.UserManager.Id;
            var repository = CreateRepository();
            IEnumerable<int> keys = [userId1, userId2];
            var entities = repository.GetMany(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Id == k) is not null));
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var userId1 = DefaultData.UserAdmin.Id;
            var userId2 = DefaultData.UserManager.Id;
            var repository = CreateRepository();
            IEnumerable<int> keys = [userId1, userId2];
            var entities = await repository.GetManyAsync(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Id == k) is not null));
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<int> keys = [DefaultData.UserAdmin.Key, DefaultData.UserManager.Key];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetManyAsync(keys, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private Methods 

        private IEntityReadonlyRepository<UserTc, int> CreateRepository()
        {
            return new UserTcReadonlyReporsitory(_dbContext);
        }

        #endregion
    }
}