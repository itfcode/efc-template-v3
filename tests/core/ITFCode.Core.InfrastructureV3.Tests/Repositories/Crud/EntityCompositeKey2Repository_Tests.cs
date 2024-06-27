using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Frameworks;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey2Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2) key)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            var productId = DefaultData.ProductA.Id;
            var countryCode = DefaultData.ProductA.CountryCode;

            var product = repository.Get((productId, countryCode));

            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal(countryCode, product.CountryCode);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        #endregion

        #region Tests: GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();
            var productId = DefaultData.ProductA.Id;
            var countryCode = DefaultData.ProductA.CountryCode;

            var product = await repository.GetAsync((productId, countryCode));

            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
            Assert.Equal(countryCode, product.CountryCode);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync((1, "key_value"), cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2)> keys)

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            var productId1 = DefaultData.ProductA.Id;
            var countryCode1 = DefaultData.ProductA.CountryCode;
            var productId2 = DefaultData.ProductB.Id;
            var countryCode2 = DefaultData.ProductB.CountryCode;
            IEnumerable<(long, string)> keys = [(productId1, countryCode1), (productId2, countryCode2)];

            var products = repository.GetMany(keys);

            Assert.NotEmpty(products);

            Assert.True(products.All(p => keys.Contains((p.Key1, p.Key2))));
            Assert.True(products.All(p => EntityState.Detached == _dbContext.Entry(p).State));
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();

            var productId1 = DefaultData.ProductA.Id;
            var countryCode1 = DefaultData.ProductA.CountryCode;
            var productId2 = DefaultData.ProductB.Id;
            var countryCode2 = DefaultData.ProductB.CountryCode;
            IEnumerable<(long, string)> keys = [(productId1, countryCode1), (productId2, countryCode2)];

            var products = await repository.GetManyAsync(keys);

            Assert.NotEmpty(products);

            Assert.True(products.All(p => keys.Contains((p.Key1, p.Key2))));
            Assert.True(products.All(p => EntityState.Detached == _dbContext.Entry(p).State));
        }

        [Fact]
        public override Task GetManyAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Insert(TEntity entity)

        [Fact]
        public override void Insert_If_Param_Is_Correct_Then_Ok()
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
            Assert.Null(_dbContext.Products.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(_dbContext.Products.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        #endregion

        #region Tests: InsertAsync(TEntity entity, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertAsync_If_Param_Is_Correct_Then_Ok()
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
            Assert.Null(_dbContext.Products.FirstOrDefault(predicate));

            await repository.CommitAsync();

            Assert.NotNull(_dbContext.Products.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        [Fact]
        public override async Task InsertAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertAsync(DefaultData.ProductA, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: InsertRange(IEnumerable<TEntity> entities)

        [Fact]
        public override void InsertRange_If_Param_Is_Correct_Then_Ok()
        {

            var product = DefaultData.ProductA;
            Expression<Func<ProductTc, bool>> predicate = x => x.Id == product.Id && x.CountryCode == product.CountryCode;
            Assert.Null(_dbContext.Products.FirstOrDefault(predicate));

            var repository = CreateRepository();

            var entity = repository.Insert(product);

            Assert.NotNull(product);
            Assert.Equal(product.Id, entity.Id);
            Assert.Equal(product.Name, entity.Name);
            Assert.Equal(product.CountryCode, entity.CountryCode);
            Assert.Equal(EntityState.Added, _dbContext.Entry(product).State);
            Assert.Null(_dbContext.Products.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(_dbContext.Products.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(product).State);
        }

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task InsertRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Update((TKey1, TKey2) key, Action<TEntity> updater)

        [Fact]
        public override void Update_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, CancellationToken cancellationToken = default)

        [Fact]
        public override Task UpdateAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task UpdateAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(DefaultData.ProductA, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater)

        [Fact]
        public override void UpdateRange_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default) 

        [Fact]
        public override async Task UpdateRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task UpdateRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: Delete((TKey1, TKey2) key)

        [Fact]
        public override void Delete_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task DeleteAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteAsync(DefaultData.ProductA, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: DeleteRange(IEnumerable<(TKey1, TKey2)> keys) 

        [Fact]
        public override void DeleteRange_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public override async Task DeleteRangeAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
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
