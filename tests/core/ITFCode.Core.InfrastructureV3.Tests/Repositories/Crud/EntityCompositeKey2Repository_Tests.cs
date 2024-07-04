using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey2Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get, GetAsync, GetMany & GetManyAsync

        [Fact] // Get((TKey1, TKey2) key)
        public override void Get_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            (long, string) key = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);

            var product = repository.Get(key);

            Assert.NotNull(product);
            Assert.Equal(key.Item1, product.Id);
            Assert.Equal(key.Item2, product.CountryCode);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();
            (long, string) key = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);

            var product = await repository.GetAsync(key);

            Assert.NotNull(product);
            Assert.Equal(key.Item1, product.Id);
            Assert.Equal(key.Item2, product.CountryCode);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync((1, "key_value"), cancellationToken: cancellationToken));
        }

        [Fact] // GetMany(IEnumerable<(TKey1, TKey2)> keys)
        public override void GetRange_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            (long, string) key1 = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);
            (long, string) key2 = (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode);

            IEnumerable<(long, string)> keys = [key1, key2];

            var products = repository.GetRange(keys);

            Assert.NotEmpty(products);

            Assert.True(products.All(p => keys.Contains((p.Key1, p.Key2))));
            Assert.True(products.All(p => EntityState.Detached == _dbContext.Entry(p).State));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        public override async Task GetRangeAsync_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);
            (long, string) key2 = (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode);

            IEnumerable<(long, string)> keys = [key1, key2];

            var products = await repository.GetRangeAsync(keys);

            Assert.NotEmpty(products);

            Assert.True(products.All(p => keys.Contains((p.Key1, p.Key2))));
            Assert.True(products.All(p => EntityState.Detached == _dbContext.Entry(p).State));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            (long, string) key1 = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);
            (long, string) key2 = (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode);

            IEnumerable<(long, string)> keys = [key1, key2];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetRangeAsync(keys, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Insert, InsertAsync, InsertRange & InsertRangeAsync

        [Fact] // Insert(TEntity entity)
        public override void Insert_Ok()
        {
            var product = DefaultData.ProductA;

            var repository = CreateRepository();

            var entity = repository.Insert(product);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == product.Id && x.CountryCode == product.CountryCode;

            Assert.NotNull(product);
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.CountryCode, entity.CountryCode);
            Assert.Equal(EntityState.Added, _dbContext.Entry(product).State);
            Assert.Null(ProductSet.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(ProductSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task InsertAsync_Ok()
        {
            var product = DefaultData.ProductA;

            var repository = CreateRepository();

            var entity = await repository.InsertAsync(product);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == product.Id && x.CountryCode == product.CountryCode;

            Assert.NotNull(product);
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.CountryCode, entity.CountryCode);
            Assert.Equal(EntityState.Added, _dbContext.Entry(product).State);
            Assert.Null(ProductSet.FirstOrDefault(predicate));

            await repository.CommitAsync();

            Assert.NotNull(ProductSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task InsertAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            var product = DefaultData.ProductA;

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertAsync(product, cancellationToken: cancellationToken));
        }

        [Fact] // InsertRange(IEnumerable<TEntity> entities)
        public override void InsertRange_Ok()
        {
            var product = DefaultData.ProductA;
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == product.Id && x.CountryCode == product.CountryCode;
            Assert.Null(ProductSet.FirstOrDefault(predicate));

            var repository = CreateRepository();

            var entity = repository.Insert(product);

            Assert.NotNull(product);
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.CountryCode, entity.CountryCode);
            Assert.Equal(EntityState.Added, _dbContext.Entry(product).State);
            Assert.Null(ProductSet.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(ProductSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task InsertRangeAsync_Ok()
        {
            var product = DefaultData.ProductA;
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == product.Id && x.CountryCode == product.CountryCode;

            Assert.Null(await ProductSet.FirstOrDefaultAsync(predicate));

            var repository = CreateRepository();
            var entity = await repository.InsertAsync(product);

            Assert.NotNull(product);
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.CountryCode, entity.CountryCode);
            Assert.Equal(EntityState.Added, _dbContext.Entry(product).State);
            Assert.Null(ProductSet.FirstOrDefault(predicate));

            await repository.CommitAsync();

            Assert.NotNull(await ProductSet.FirstOrDefaultAsync(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task InsertRangeAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<ProductTc> products = [DefaultData.ProductA, DefaultData.ProductB];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertRangeAsync(products, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Update, UpdateAsync, UpdateMany & UpdateManyAsync

        [Fact] // Update((TKey1, TKey2) key, Action<TEntity> updater)
        public override void Update_By_Key_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var product = ProductSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(product);

            repository.Update(key, x => x.Name = $"New{x.Name}");

            var productBefore = ProductSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(productBefore);
            Assert.NotEqual($"New{product.Name}", productBefore.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);

            repository.Commit();

            var productAfter = ProductSet.FirstOrDefault(predicate);

            Assert.NotNull(productAfter);
            Assert.Equal($"New{product.Name}", productAfter.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, CancellationToken cancellationToken = default)
        public override async Task UpdateAsync_By_Key_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var product = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(product);

            await repository.UpdateAsync(key, x => x.Name = $"New{x.Name}");

            var productBefore = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(productBefore);
            Assert.NotEqual($"New{product.Name}", productBefore.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);

            repository.Commit();

            var productAfter = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(productAfter);
            Assert.Equal($"New{product.Name}", productAfter.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, CancellationToken cancellationToken = default)
        public override async Task UpdateAsync_By_Key_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(key, x => x.Name = "New Name", cancellationToken: cancellationToken));
        }


        [Fact] // Update(TEntity entity)
        public override void Update_By_Entity_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var product = ProductSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(product);

            var newName = $"New{product.Name}";
            product.Name = newName;
            repository.Update(product);

            var productBefore = ProductSet.AsNoTracking().FirstOrDefault(predicate);

            Assert.NotNull(productBefore);
            Assert.NotEqual(newName, productBefore.Name);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(product).State);

            repository.Commit();

            var productAfter = ProductSet.FirstOrDefault(predicate);

            Assert.NotNull(productAfter);
            Assert.Equal(newName, productAfter.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task UpdateAsync_By_Entity_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var product = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(product);

            var newName = $"New{product.Name}";
            product.Name = newName;
            await repository.UpdateAsync(product);

            var productBefore = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate);

            Assert.NotNull(productBefore);
            Assert.NotEqual(newName, productBefore.Name);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(product).State);

            await repository.CommitAsync();

            var productAfter = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(productAfter);
            Assert.Equal(newName, productAfter.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact] // UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        public override async Task UpdateAsync_By_Entity_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            var product = DefaultData.ProductA;

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(product, cancellationToken: cancellationToken));
        }


        [Fact] // UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater)
        public override void UpdateRange_By_Keys_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);
            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var product1 = ProductSet.AsNoTracking().FirstOrDefault(predicate1);
            var product2 = ProductSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(product1);
            Assert.NotNull(product2);

            repository.UpdateRange([key1, key2], x => x.Name = $"New{x.Name}");

            var productBefore1 = ProductSet.AsNoTracking().FirstOrDefault(predicate1);
            var productBefore2 = ProductSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);
            Assert.NotEqual($"New{product1.Name}", productBefore1.Name);
            Assert.NotEqual($"New{product2.Name}", productBefore2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);

            repository.Commit();

            var productAfter1 = ProductSet.FirstOrDefault(predicate1);
            var productAfter2 = ProductSet.FirstOrDefault(predicate2);

            Assert.NotNull(productAfter1);
            Assert.NotNull(productAfter2);
            Assert.Equal($"New{product1.Name}", productAfter1.Name);
            Assert.Equal($"New{product2.Name}", productAfter2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);
        }

        [Fact] // UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default) 
        public override async Task UpdateRangeAsync_By_Keys_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);
            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var product1 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var product2 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(product1);
            Assert.NotNull(product2);

            await repository.UpdateRangeAsync([key1, key2], x => x.Name = $"New{x.Name}");

            var productBefore1 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var productBefore2 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);
            Assert.NotEqual($"New{product1.Name}", productBefore1.Name);
            Assert.NotEqual($"New{product2.Name}", productBefore2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);

            repository.Commit();

            var productAfter1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productAfter2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productAfter1);
            Assert.NotNull(productAfter2);
            Assert.Equal($"New{product1.Name}", productAfter1.Name);
            Assert.Equal($"New{product2.Name}", productAfter2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);
        }

        [Fact] // UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default) 
        public override async Task UpdateRangeAsync_By_Key_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<(long, string)> keys = [(DefaultData.ProductA.Key1, DefaultData.ProductA.Key2),
                (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2),];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(keys, x => x.Name = "New Name", cancellationToken: cancellationToken));
        }


        [Fact] // UpdateRange(IEnumerable<TEntity> entities)
        public override void UpdateRange_By_Enities_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);
            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var product1 = ProductSet.AsNoTracking().FirstOrDefault(predicate1);
            var product2 = ProductSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(product1);
            Assert.NotNull(product2);

            repository.UpdateRangeAsync([key1, key2], x => x.Name = $"New{x.Name}");

            var productBefore1 = ProductSet.AsNoTracking().FirstOrDefault(predicate1);
            var productBefore2 = ProductSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);
            Assert.NotEqual($"New{product1.Name}", productBefore1.Name);
            Assert.NotEqual($"New{product2.Name}", productBefore2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);

            repository.Commit();

            var productAfter1 =  ProductSet.FirstOrDefault(predicate1);
            var productAfter2 =  ProductSet.FirstOrDefault(predicate2);

            Assert.NotNull(productAfter1);
            Assert.NotNull(productAfter2);
            Assert.Equal($"New{product1.Name}", productAfter1.Name);
            Assert.Equal($"New{product2.Name}", productAfter2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);
        }

        [Fact] // UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task UpdateRangeAsync_By_Entities_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);
            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var product1 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var product2 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(product1);
            Assert.NotNull(product2);

            await repository.UpdateRangeAsync([key1, key2], x => x.Name = $"New{x.Name}");

            var productBefore1 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var productBefore2 = await ProductSet.AsNoTracking().FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);
            Assert.NotEqual($"New{product1.Name}", productBefore1.Name);
            Assert.NotEqual($"New{product2.Name}", productBefore2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);

            repository.Commit();

            var productAfter1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productAfter2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productAfter1);
            Assert.NotNull(productAfter2);
            Assert.Equal($"New{product1.Name}", productAfter1.Name);
            Assert.Equal($"New{product2.Name}", productAfter2.Name);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product2).State);
        }

        [Fact] // UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        public override async Task UpdateRangeAsync_By_Entities_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<ProductTc> products = [DefaultData.ProductA, DefaultData.ProductB];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(products, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Delete, DeleteAsync, DeleteMany & DeleteManyAsync

        [Fact] // Delete((TKey1, TKey2) key)
        public override void Delete_By_Key_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            repository.Delete(key);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var productBefore = ProductSet.FirstOrDefault(predicate);

            Assert.NotNull(productBefore);

            repository.Commit();

            var productAfter = ProductSet.FirstOrDefault(predicate);

            Assert.Null(productAfter);
        }

        [Fact] // DeleteAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        public override async Task DeleteAsync_By_Key_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            await repository.DeleteAsync(key);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var productBefore = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(productBefore);

            await repository.CommitAsync();

            var productAfter = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.Null(productAfter);
        }

        [Fact]
        public override async Task DeleteAsync_By_Key_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            (long, string) key = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(key, cancellationToken: cancellationToken));
        }

        [Fact]
        public override async Task DeleteAsync_By_Entity_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(DefaultData.ProductA, cancellationToken: cancellationToken));
        }

        [Fact] // DeleteRange(IEnumerable<(TKey1, TKey2)> keys) 
        public override void DeleteRange_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);

            repository.DeleteRange([key1, key2]);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var productBefore1 = ProductSet.FirstOrDefault(predicate1);
            var productBefore2 = ProductSet.FirstOrDefault(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);

            repository.Commit();

            var productAfter1 = ProductSet.FirstOrDefault(predicate1);
            var productAfter2 = ProductSet.FirstOrDefault(predicate2);

            Assert.Null(productAfter1);
            Assert.Null(productAfter2);
        }

        [Fact] // DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        public override async Task DeleteRangeAsync_By_Keys_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);

            await repository.DeleteRangeAsync([key1, key2]);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var productBefore1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productBefore2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);

            repository.Commit();

            var productAfter1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productAfter2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.Null(productAfter1);
            Assert.Null(productAfter2);
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<ProductTc> products = [DefaultData.ProductA, DefaultData.ProductB];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(products, cancellationToken: cancellationToken));

            IEnumerable<(long, string)> keys = [(DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode),
                (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode)];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(keys, cancellationToken: cancellationToken));
        }

        [Fact]
        public override void Delete_By_Entity_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            var entity = ProductSet.AsNoTracking().FirstOrDefault();
            Assert.NotNull(entity);

            (long, string) key = (entity.Key1, entity.Key2);

            repository.Delete(entity);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var productBefore = ProductSet.FirstOrDefault(predicate);

            Assert.NotNull(productBefore);

            repository.Commit();

            var productAfter = ProductSet.FirstOrDefault(predicate);

            Assert.Null(productAfter);
        }

        [Fact]
        public override async Task DeleteAsync_By_Entity_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            var entity = await ProductSet.AsNoTracking().FirstOrDefaultAsync();
            Assert.NotNull(entity);

            (long, string) key = (entity.Key1, entity.Key2);

            await repository.DeleteAsync(entity);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == key.Item1 && x.CountryCode == key.Item2;

            var productBefore = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.NotNull(productBefore);

            await repository.CommitAsync();

            var productAfter = await ProductSet.FirstOrDefaultAsync(predicate);

            Assert.Null(productAfter);
        }

        [Fact]
        public override void DeleteRange_By_Entities_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);

            repository.DeleteRange([DefaultData.ProductA, DefaultData.ProductB]);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var productBefore1 = ProductSet.FirstOrDefault(predicate1);
            var productBefore2 = ProductSet.FirstOrDefault(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);

            repository.Commit();

            var productAfter1 = ProductSet.FirstOrDefault(predicate1);
            var productAfter2 = ProductSet.FirstOrDefault(predicate2);

            Assert.Null(productAfter1);
            Assert.Null(productAfter2);
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Entities_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            (long, string) key1 = (DefaultData.ProductA.Key1, DefaultData.ProductA.Key2);
            (long, string) key2 = (DefaultData.ProductB.Key1, DefaultData.ProductB.Key2);

            await repository.DeleteRangeAsync([DefaultData.ProductA, DefaultData.ProductB]);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == key1.Item1 && x.CountryCode == key1.Item2;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == key2.Item1 && x.CountryCode == key2.Item2;

            var productBefore1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productBefore2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.NotNull(productBefore1);
            Assert.NotNull(productBefore2);

            await repository.CommitAsync();

            var productAfter1 = await ProductSet.FirstOrDefaultAsync(predicate1);
            var productAfter2 = await ProductSet.FirstOrDefaultAsync(predicate2);

            Assert.Null(productAfter1);
            Assert.Null(productAfter2);
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Entities_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<ProductTc> products = [DefaultData.ProductA, DefaultData.ProductB];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(products, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private Methods 

        private IEntityRepository<ProductTc, long, string> CreateRepository()
        {
            return new ProductTcReporsitory(_dbContext);
        }

        #endregion
    }
}