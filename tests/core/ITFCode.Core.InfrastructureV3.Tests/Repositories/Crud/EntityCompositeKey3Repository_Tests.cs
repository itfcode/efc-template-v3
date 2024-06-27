using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey3Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            var companyId = DefaultData.ProductOrder1.Key1;
            var locationId = DefaultData.ProductOrder1.Key2;
            var userId = DefaultData.ProductOrder1.Key3;

            var productOrder = repository.Get((companyId, locationId, userId));

            Assert.NotNull(productOrder);
            Assert.Equal(companyId, productOrder.CompanyId);
            Assert.Equal(locationId, productOrder.LocationId);
            Assert.Equal(userId, productOrder.UserId);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(productOrder).State);
        }

        #endregion

        #region Tests: GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            var companyId = DefaultData.ProductOrder1.Key1;
            var locationId = DefaultData.ProductOrder1.Key2;
            var userId = DefaultData.ProductOrder1.Key3;

            var productOrder = await repository.GetAsync((companyId, locationId, userId));

            Assert.NotNull(productOrder);
            Assert.Equal(companyId, productOrder.CompanyId);
            Assert.Equal(locationId, productOrder.LocationId);
            Assert.Equal(userId, productOrder.UserId);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(productOrder).State);
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
            throw new NotImplementedException();
        }

        [Fact]
        public override Task GetManyAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) 

        [Fact]
        public override Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Insert(TEntity entity)

        [Fact]
        public override void Insert_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: InsertAsync(TEntity entity, CancellationToken cancellationToken = default)

        [Fact]
        public override Task InsertAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task InsertAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: InsertRange(IEnumerable<TEntity> entities)

        [Fact]
        public override void InsertRange_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        [Fact]
        public override Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task InsertRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Update((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false)

        [Fact]
        public override void Update_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task UpdateAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task UpdateAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false)

        [Fact]
        public override void UpdateRange_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task UpdateRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task UpdateRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Delete((TKey1, TKey2, TKey3) key, bool shouldSave = false)

        [Fact]
        public override void Delete_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteAsync((TKey1, TKey2, TKey3) key, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task DeleteAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false)

        [Fact]
        public override void DeleteRange_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task DeleteRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods 

        private IEntityRepository<ProductOrderTc, Guid, string, int> CreateRepository()
        {
            return new ProductOrderTcReporsitory(_dbContext);
        }

        #endregion
    }
}