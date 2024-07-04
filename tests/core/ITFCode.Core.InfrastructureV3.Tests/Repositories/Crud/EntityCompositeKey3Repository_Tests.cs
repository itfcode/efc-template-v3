using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey3Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get, GetAsync, GetMany & GetManyAsync

        [Fact] // Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)
        public override void Get_Ok()
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

        [Fact] // GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Ok()
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

        [Fact] // GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync(key, cancellationToken: cancellationToken));
        }

        [Fact] // GetMany(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true)
        public override void GetRange_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = repository.GetRange(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) 
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = await repository.GetRangeAsync(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) 
        public override async Task GetRangeAsync_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            IEnumerable<(Guid, string, int)> keys = [key1, key2];
            var productOrders = await repository.GetRangeAsync(keys);

            Assert.NotEmpty(productOrders);
            Assert.Equal(2, productOrders.Count);
            Assert.True(keys.All(k => productOrders.FirstOrDefault(o => o.Key1 == k.Item1 && o.Key2 == k.Item2 && o.Key3 == k.Item3) is not null));
            Assert.True(productOrders.All(o => _dbContext.Entry(o).State == EntityState.Detached));
        }

        #endregion

        #region Tests: Insert, InsertAsync, InsertRange & InsertRangeAsync

        [Fact] // Insert(TEntity entity)
        public override void Insert_Ok()
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

        [Fact] // InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task InsertAsync_Ok()
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

        [Fact] // InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task InsertAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertAsync(DefaultData.ProductOrder1, cancellationToken));
        }


        [Fact] // InsertRange(IEnumerable<TEntity> entities)
        public override void InsertRange_Ok()
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

        [Fact] // InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task InsertRangeAsync_Ok()
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

        [Fact] // InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task InsertRangeAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertRangeAsync([DefaultData.ProductOrder1, DefaultData.ProductOrder1], cancellationToken));
        }

        #endregion

        #region Tests: Update, UpdateAsync, UpdateMany & UpdateManyAsync

        [Fact] // Update((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false)
        public override void Update_By_Key_Ok()
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

        [Fact] // UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        public override async Task UpdateAsync_By_Key_Ok()
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


        [Fact] // UpdateRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false)
        public override void UpdateRange_By_Keys_Ok()
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

        [Fact] // UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        public override async Task UpdateRangeAsync_By_Keys_Ok()
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

        [Fact] // UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)
        public override async Task UpdateRangeAsync_By_Key_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<(Guid, string, int)> keys = [
                (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3),
                (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3)
            ];
            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(keys, p => p.Code = 10, cancellationToken));
        }


        [Fact]
        public override void Update_By_Entity_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(order);

            var newOrder = order.Code + 5555;
            order.Code = newOrder;
            repository.Update(order);

            var orderBefore = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(orderBefore);
            Assert.NotEqual(newOrder, orderBefore.Code);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(order).State);

            Assert.Equal(1, repository.Commit());

            var orderAfter = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(orderAfter);
            Assert.Equal(newOrder, orderAfter.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
        }

        [Fact]
        public override async Task UpdateAsync_By_Entity_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(order);

            var newOrder = order.Code + 5555;
            order.Code = newOrder;
            await repository.UpdateAsync(order);

            var orderBefore = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderBefore);
            Assert.NotEqual(newOrder, orderBefore.Code);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(order).State);

            Assert.Equal(1, await repository.CommitAsync());

            var orderAfter = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderAfter);
            Assert.Equal(newOrder, orderAfter.Code);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(order).State);
        }

        [Fact]
        public override async Task UpdateAsync_By_Entity_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(DefaultData.ProductOrder1, cancellationToken));
        }


        [Fact]
        public override void UpdateRange_By_Enities_Ok()
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
        public override async Task UpdateRangeAsync_By_Entities_Ok()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync([DefaultData.ProductOrder1, DefaultData.ProductOrder2], cancellationToken));
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

        #region Tests: Delete, DeleteAsync, DeleteMany & DeleteManyAsync

        [Fact] // Delete((TKey1, TKey2, TKey3) key, bool shouldSave = false)
        public override void Delete_By_Key_Ok()
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

        [Fact] // DeleteAsync((TKey1, TKey2, TKey3) key, bool shouldSave = false, CancellationToken cancellationToken = default)
        public override async Task DeleteAsync_By_Key_Ok()
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
        public override async Task DeleteAsync_By_Key_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(DefaultData.ProductOrder1, cancellationToken));
        }

        [Fact]
        public override void Delete_By_Entity_Ok()
        {
            AddTestingData();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            var repository = CreateRepository();

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = ProductOrderSet.AsNoTracking().FirstOrDefault(predicate);
            Assert.NotNull(order);

            repository.Delete(order);

            var orderBefore = ProductOrderSet.FirstOrDefault(predicate);

            Assert.NotNull(orderBefore);

            repository.Commit();

            var orderAfter = ProductOrderSet.FirstOrDefault(predicate);

            Assert.Null(orderAfter);
        }

        [Fact]
        public override async Task DeleteAsync_By_Entity_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            var repository = CreateRepository();

            Expression<Func<ProductOrderTc, bool>> predicate = x => x.CompanyId == key.Item1 && x.LocationId == key.Item2 && x.UserId == key.Item3;

            var order = await ProductOrderSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            Assert.NotNull(order);

            await repository.DeleteAsync(order);

            var orderBefore = await ProductOrderSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(orderBefore);

            await repository.CommitAsync();

            var orderAfter = await ProductOrderSet.FirstOrDefaultAsync(predicate);

            Assert.Null(orderAfter);
        }

        [Fact] // DeleteAsync((TKey1, TKey2, TKey3) key, bool shouldSave = false, CancellationToken cancellationToken = default)
        public override async Task DeleteAsync_By_Entity_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            var key = (Guid.NewGuid(), "value", 1);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(key, cancellationToken));
        }


        [Fact] // DeleteRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false)
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

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_Ok()
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


        [Fact]
        public override void DeleteRange_By_Entities_Ok()
        {
            AddTestingData();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            var repository = CreateRepository();

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var order1 = ProductOrderSet.FirstOrDefault(predicate1);
            var order2 = ProductOrderSet.FirstOrDefault(predicate2);

            Assert.NotNull(order1);
            Assert.NotNull(order2);

            repository.DeleteRange([key1, key2]);

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

        [Fact]
        public override async Task DeleteRangeAsync_By_Entities_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3);

            Expression<Func<ProductOrderTc, bool>> predicate1 = x => x.CompanyId == key1.Item1 && x.LocationId == key1.Item2 && x.UserId == key1.Item3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = x => x.CompanyId == key2.Item1 && x.LocationId == key2.Item2 && x.UserId == key2.Item3;

            var repository = CreateRepository();

            var order1 = await ProductOrderSet.FirstOrDefaultAsync(predicate1);
            var order2 = await ProductOrderSet.FirstOrDefaultAsync(predicate2);
            Assert.NotNull(order1);
            Assert.NotNull(order2);

            repository.DeleteRange([order1, order2]);

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
        public override async Task DeleteRangeAsync_By_Entities_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

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