using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntityCompositeKey3ReadonlyRepositoryTests : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var companyId = DefaultData.ProductOrder1.CompanyId;
            var locationId = DefaultData.ProductOrder1.LocationId;
            var userId = DefaultData.ProductOrder1.UserId;

            var repository = CreateRepository();
            var entity = repository.Get((companyId, locationId, userId));

            Assert.NotNull(entity);
            Assert.Equal(companyId, entity.Key1);
            Assert.Equal(locationId, entity.Key2);
            Assert.Equal(userId, entity.Key3);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        #endregion

        #region Tests: GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var companyId = DefaultData.ProductOrder1.CompanyId;
            var locationId = DefaultData.ProductOrder1.LocationId;
            var userId = DefaultData.ProductOrder1.UserId;

            var repository = CreateRepository();
            var entity = await repository.GetAsync((companyId, locationId, userId));

            Assert.NotNull(entity);
            Assert.Equal(companyId, entity.Key1);
            Assert.Equal(locationId, entity.Key2);
            Assert.Equal(userId, entity.Key3);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
                
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync(key, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true)

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var companyId1 = DefaultData.ProductOrder1.CompanyId;
            var locationId1 = DefaultData.ProductOrder1.LocationId;
            var userId1 = DefaultData.ProductOrder1.UserId;

            var companyId2 = DefaultData.ProductOrder2.CompanyId;
            var locationId2 = DefaultData.ProductOrder2.LocationId;
            var userId2 = DefaultData.ProductOrder2.UserId;

            var repository = CreateRepository();
            IEnumerable<(Guid, string, int)> keys = [
                (companyId1, locationId1, userId1),
                (companyId2, locationId2, userId2)];

            var entities = repository.GetMany(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2 && u.Key3 == k.Item3) is not null));
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var companyId1 = DefaultData.ProductOrder1.CompanyId;
            var locationId1 = DefaultData.ProductOrder1.LocationId;
            var userId1 = DefaultData.ProductOrder1.UserId;

            var companyId2 = DefaultData.ProductOrder2.CompanyId;
            var locationId2 = DefaultData.ProductOrder2.LocationId;
            var userId2 = DefaultData.ProductOrder2.UserId;

            var repository = CreateRepository();
            IEnumerable<(Guid, string, int)> keys = [
                (companyId1, locationId1, userId1),
                (companyId2, locationId2, userId2)];

            var entities = await repository.GetManyAsync(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2 && u.Key3 == k.Item3) is not null));
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<(Guid, string, int)> keys = [
                (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3),
                (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3)];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetManyAsync(keys, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private Methods 

        private IEntityReadonlyRepository<ProductOrderTc, Guid, string, int> CreateRepository()
        {
            return new ProductOrderTcReadonlyReporsitory(_dbContext);
        }

        #endregion
    }
}