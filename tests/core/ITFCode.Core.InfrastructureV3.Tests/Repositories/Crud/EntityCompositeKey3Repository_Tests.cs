using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

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

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);

            var productOrder = repository.Get(key);

            Assert.NotNull(productOrder);
            Assert.Equal(key.Item1, productOrder.CompanyId);
            Assert.Equal(key.Item2, productOrder.LocationId);
            Assert.Equal(key.Item3, productOrder.UserId);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(productOrder).State);
        }

        #endregion

        #region Tests: GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);

            var productOrder = await repository.GetAsync(key);

            Assert.NotNull(productOrder);
            Assert.Equal(key.Item1, productOrder.CompanyId);
            Assert.Equal(key.Item2, productOrder.LocationId);
            Assert.Equal(key.Item3, productOrder.UserId);
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
        public override async Task InsertAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertAsync(DefaultData.ProductOrder1, cancellationToken));
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
        public override async Task InsertRangeAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertRangeAsync([DefaultData.ProductOrder1, DefaultData.ProductOrder1], cancellationToken));
        }

        #endregion

        #region Tests: Update((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false)

        [Fact]
        public override void Update_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task UpdateAsync_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override void UpdateRange_If_Params_Are_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task UpdateAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(DefaultData.ProductOrder1, cancellationToken));

            var key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(key, p => p.Code = 10, cancellationToken));
        }

        #endregion

        #region Tests: UpdateRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false)

        [Fact]
        public override void UpdateRange_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override Task UpdateRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateRangeAsync_If_Params_Are_Correct_Then_Ok()
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
        public override void Delete_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            var repository = CreateRepository();

            repository.Delete(key);

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var orderBefore = ProductOrderSet.FirstOrDefault(predicate);

            Assert.NotNull(orderBefore);

            repository.Commit();

            var orderAfter = ProductOrderSet.FirstOrDefault(predicate);

            Assert.Null(orderAfter);
        }

        #endregion

        #region Tests: DeleteAsync((TKey1, TKey2, TKey3) key, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteAsync_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            var repository = CreateRepository();

            await repository.DeleteAsync(key);

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var orderBefore = await ProductOrderSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderBefore);

            repository.Commit();

            var orderAfter = await ProductOrderSet.FirstOrDefaultAsync(predicate);

            Assert.Null(orderAfter);
        }

        [Fact]
        public override async Task DeleteAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            var key = (Guid.NewGuid(), "value", 1);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(key, cancellationToken));
        }

        #endregion

        #region Tests: DeleteRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false)

        [Fact]
        public override void DeleteRange_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            var repository = CreateRepository();

            repository.DeleteRange([key1, key2]);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var orderBefore1 = ProductOrderSet.FirstOrDefault(predicate1);
            var orderBefore2 = ProductOrderSet.FirstOrDefault(predicate2);

            Assert.NotNull(orderBefore1);
            Assert.NotNull(orderBefore2);

            repository.Commit();

            var orderAfter1 = ProductOrderSet.FirstOrDefault(predicate1);
            var orderAfter2 = ProductOrderSet.FirstOrDefault(predicate2);

            Assert.Null(orderAfter1);
            Assert.Null(orderAfter2);
        }

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            var repository = CreateRepository();

            await repository.DeleteRangeAsync([key1, key2]);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var orderBefore1 = await ProductOrderSet.FirstOrDefaultAsync(predicate1);
            var orderBefore2 = await ProductOrderSet.FirstOrDefaultAsync(predicate2);

            Assert.NotNull(orderBefore1);
            Assert.NotNull(orderBefore2);

            await repository.CommitAsync();

            var orderAfter1 = await ProductOrderSet.FirstOrDefaultAsync(predicate1);
            var orderAfter2 = await ProductOrderSet.FirstOrDefaultAsync(predicate2);

            Assert.Null(orderAfter1);
            Assert.Null(orderAfter2);
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<(Guid, string, int)> keys = [(Guid.NewGuid(), "value1", 1), (Guid.NewGuid(), "value2", 2)];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(keys, cancellationToken));

            IEnumerable<ProductOrderTc> productOrders = [DefaultData.ProductOrder1, DefaultData.ProductOrder2];
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(productOrders, cancellationToken));
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