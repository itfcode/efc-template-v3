using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = repository.GetMany(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = await repository.GetManyAsync(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) 

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = await repository.GetManyAsync(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        #endregion

        #region Tests: Insert(TEntity entity)

        [Fact]
        public override void Insert_If_Param_Is_Correct_Then_Ok()
        {
            var order = DefaultData.ProductOrder1;

            var repository = CreateRepository();

            var entity = repository.Insert(order);

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == order.CompanyId && x.LocationId == order.LocationId && x.UserId == order.UserId;

            Assert.NotNull(order);
            Assert.Equal(EntityState.Added, _dbContext.Entry(order).State);
            Assert.Null(ProductOrderSet.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
        }

        #endregion

        #region Tests: InsertAsync(TEntity entity, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertAsync_If_Param_Is_Correct_Then_Ok()
        {
            var order = DefaultData.ProductOrder1;
            var repository = CreateRepository();
            var entity = await repository.InsertAsync(order);

            Assert.NotNull(entity);

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == order.CompanyId && x.LocationId == order.LocationId && x.UserId == order.UserId;

            Assert.NotNull(order);
            Assert.Equal(EntityState.Added, _dbContext.Entry(order).State);
            Assert.Null(await ProductOrderSet.FirstOrDefaultAsync(predicate));

            repository.Commit();

            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
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
            var order1 = DefaultData.ProductOrder1;
            var order2 = DefaultData.ProductOrder2;
            var repository = CreateRepository();

            repository.InsertRange([order1, order2]);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == order1.CompanyId && x.LocationId == order1.LocationId && x.UserId == order1.UserId;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == order2.CompanyId && x.LocationId == order2.LocationId && x.UserId == order2.UserId;

            Assert.Equal(EntityState.Added, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Added, _dbContext.Entry(order2).State);

            Assert.Null(ProductOrderSet.FirstOrDefault(predicate1));
            Assert.Null(ProductOrderSet.FirstOrDefault(predicate2));

            repository.Commit();

            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate1));
            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate2));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);
        }

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            var order1 = DefaultData.ProductOrder1;
            var order2 = DefaultData.ProductOrder2;
            var repository = CreateRepository();

            await repository.InsertRangeAsync([order1, order2]);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == order1.CompanyId && x.LocationId == order1.LocationId && x.UserId == order1.UserId;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == order2.CompanyId && x.LocationId == order2.LocationId && x.UserId == order2.UserId;

            Assert.Equal(EntityState.Added, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Added, _dbContext.Entry(order2).State);

            Assert.Null(await ProductOrderSet.FirstOrDefaultAsync(predicate1));
            Assert.Null(await ProductOrderSet.FirstOrDefaultAsync(predicate2));

            repository.Commit();

            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate1));
            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate2));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);
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
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(order);

            repository.Update(key, x => x.Code += 5555);

            var orderBefore = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(orderBefore);
            Assert.NotEqual(order.Code + 5555, orderBefore.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);

            Assert.Equal(1, repository.Commit());

            var orderAfter = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(orderAfter);
            Assert.Equal(order.Code + 5555, orderAfter.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
        }

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task UpdateAsync_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(order);

            repository.Update(key, x => x.Code += 5555);

            var orderBefore = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderBefore);
            Assert.NotEqual(order.Code + 5555, orderBefore.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);

            Assert.Equal(1, repository.Commit());

            var orderAfter = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderAfter);
            Assert.Equal(order.Code + 5555, orderAfter.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
        }

        [Fact]
        public override void UpdateRange_By_Enities_If_Params_Are_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var order1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var order2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(order1);
            Assert.NotNull(order2);

            order1.Code += 55;
            order2.Code += 55;

            repository.UpdateRange([order1, order2]);

            var orderBefore1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var orderBefore2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(orderBefore1);
            Assert.NotNull(orderBefore2);
            Assert.NotEqual(order1.Code, orderBefore1.Code);
            Assert.NotEqual(order2.Code, orderBefore2.Code);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(order2).State);

            Assert.Equal(2, repository.Commit());

            var orderAfter1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var orderAfter2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(orderAfter1);
            Assert.NotNull(orderAfter2);
            Assert.Equal(order1.Code, orderAfter1.Code);
            Assert.Equal(order2.Code, orderAfter2.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);
        }

        [Fact]
        public override async Task UpdateAsync_By_Key_Throw_If_Cancellation()
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
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var order1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var order2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(order1);
            Assert.NotNull(order2);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            repository.UpdateRange(keys, x => x.Code += 5555);

            var orderBefore1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var orderBefore2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(orderBefore1);
            Assert.NotNull(orderBefore2);
            Assert.NotEqual(order1.Code + 5555, orderBefore1.Code);
            Assert.NotEqual(order2.Code + 5555, orderBefore2.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);

            Assert.Equal(2, repository.Commit());

            var orderAfter1 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate1);
            var orderAfter2 = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(orderAfter1);
            Assert.NotNull(orderAfter2);
            Assert.Equal(order1.Code + 5555, orderAfter1.Code);
            Assert.Equal(order2.Code + 5555, orderAfter2.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);
        }

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task UpdateRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var order1 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var order2 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(order1);
            Assert.NotNull(order2);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            repository.UpdateRange(keys, x => x.Code += 5555);

            var orderBefore1 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var orderBefore2 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(orderBefore1);
            Assert.NotNull(orderBefore2);
            Assert.NotEqual(order1.Code + 5555, orderBefore1.Code);
            Assert.NotEqual(order2.Code + 5555, orderBefore2.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);

            Assert.Equal(2, await repository.CommitAsync());

            var orderAfter1 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var orderAfter2 = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(orderAfter1);
            Assert.NotNull(orderAfter2);
            Assert.Equal(order1.Code + 5555, orderAfter1.Code);
            Assert.Equal(order2.Code + 5555, orderAfter2.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order2).State);
        }

        [Fact]
        public override async Task UpdateRangeAsync_By_Entities_If_Params_Are_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }


        [Fact]
        public override async Task UpdateRangeAsync_By_Entities_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<ProductOrderTc> items = [DefaultData.ProductOrder1, DefaultData.ProductOrder2];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(items, cancellationToken: cancellationToken));

            IEnumerable<(Guid, string, int)> keys = items.Select(x => (x.Key1, x.Key2, x.Key3));

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(keys, x => x.Code += 1, cancellationToken: cancellationToken));
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
        public override async Task DeleteAsync_By_Entity_Throw_If_Cancellation()
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

        [Fact]
        public override void Update_By_Entity_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task UpdateAsync_By_Entity_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task UpdateAsync_By_Entity_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task UpdateRangeAsync_By_Key_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteAsync_By_Key_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override void Delete_By_Entity_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteAsync_By_Entity_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override void DeleteRange_By_Entities_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteRangeAsync_By_Entities_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override Task DeleteRangeAsync_By_Entities_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}