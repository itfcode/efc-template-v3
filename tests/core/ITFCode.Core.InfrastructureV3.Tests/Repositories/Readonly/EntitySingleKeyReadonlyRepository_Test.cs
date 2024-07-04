using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntitySingleKeyReadonlyRepository_Test : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get, GetAsync, GetMany & GetManyAsync

        [Fact] // Get(TKey key, bool asNoTracking = true)
        public override void Get_Ok()
        {
            AddTestingData();

            var key = DefaultData.UserAdmin.Id;
            var repository = CreateRepository();
            var entity = repository.Get(key);

            Assert.NotNull(entity);
            Assert.Equal(key, entity.Key);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        [Fact] // GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Ok()
        {
            AddTestingData();

            var key = DefaultData.UserAdmin.Id;
            var repository = CreateRepository();
            var enity = await repository.GetAsync(key);

            Assert.NotNull(enity);
            Assert.Equal(key, enity.Key);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(enity).State);
        }

        [Fact] // GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync(DefaultData.UserAdmin.Key, cancellationToken: cancellationToken));
        }

        [Fact] // GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)
        public override void GetRange_Ok()
        {
            AddTestingData();

            var key1 = DefaultData.UserAdmin.Id;
            var key2 = DefaultData.UserManager.Id;
            var repository = CreateRepository();
            IEnumerable<int> keys = [key1, key2];

            var entities = repository.GetRange(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Id == k) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetRangeAsync_Ok()
        {
            await AddTestingDataAsync();

            var key1 = DefaultData.UserAdmin.Id;
            var key2 = DefaultData.UserManager.Id;
            var repository = CreateRepository();
            IEnumerable<int> keys = [key1, key2];

            var entities = await repository.GetRangeAsync(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Id == k) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<int> keys = [DefaultData.UserAdmin.Key, DefaultData.UserManager.Key];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetRangeAsync(keys, cancellationToken: cancellationToken));
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