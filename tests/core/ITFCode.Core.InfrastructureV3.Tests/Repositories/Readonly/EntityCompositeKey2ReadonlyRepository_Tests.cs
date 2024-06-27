using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntityCompositeKey2ReadonlyRepository_Tests : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2) key)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var productId = DefaultData.ProductA.Id;
            var productCountryCode = DefaultData.ProductA.CountryCode;

            var repository = CreateRepository();
            var entity = repository.Get((productId, productCountryCode));

            Assert.NotNull(entity);
            Assert.Equal(productId, entity.Key1);
            Assert.Equal(productCountryCode, entity.Key2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        #endregion
       
        #region Tests: GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var productId = DefaultData.ProductA.Id;
            var productCountryCode = DefaultData.ProductA.CountryCode;

            var repository = CreateRepository();
            var entity = await repository.GetAsync((productId, productCountryCode));

            Assert.NotNull(entity);
            Assert.Equal(productId, entity.Key1);
            Assert.Equal(productCountryCode, entity.Key2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
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

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2)> keys )

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var productId1 = DefaultData.ProductA.Id;
            var productId2 = DefaultData.ProductB.Id;
            var productCountryCode1 = DefaultData.ProductA.CountryCode;
            var productCountryCode2 = DefaultData.ProductB.CountryCode;

            var repository = CreateRepository();
            IEnumerable<(long, string)> keys = [(productId1, productCountryCode1), (productId2, productCountryCode2)];

            var entities = repository.GetMany(keys);

            Assert.NotEmpty(entities);

            var entity1 = entities.FirstOrDefault(p => p.Key1 == productId1 && p.Key2 == productCountryCode1);
            var entity2 = entities.FirstOrDefault(p => p.Key1 == productId2 && p.Key2 == productCountryCode2);

            Assert.NotNull(entity1);
            Assert.NotNull(entity2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity2).State);
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var productId1 = DefaultData.ProductA.Id;
            var productId2 = DefaultData.ProductB.Id;
            var productCountryCode1 = DefaultData.ProductA.CountryCode;
            var productCountryCode2 = DefaultData.ProductB.CountryCode;

            var repository = CreateRepository();
            IEnumerable<(long, string)> keys = [(productId1, productCountryCode1), (productId2, productCountryCode2)];

            var entities = await repository.GetManyAsync(keys);

            Assert.NotEmpty(entities);

            var entity1 = entities.FirstOrDefault(p => p.Key1 == productId1 && p.Key2 == productCountryCode1);
            var entity2 = entities.FirstOrDefault(p => p.Key1 == productId2 && p.Key2 == productCountryCode2);

            Assert.NotNull(entity1);
            Assert.NotNull(entity2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity2).State);
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<(long, string)> keys = [(DefaultData.ProductA.Key1, DefaultData.ProductA.Key2)
                ,(DefaultData.ProductB.Key1, DefaultData.ProductB.Key2)];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetManyAsync(keys, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private Methods 

        private IEntityReadonlyRepository<ProductTc, long, string> CreateRepository()
        {
            return new ProductTcReadonlyReporsitory(_dbContext);
        }

        #endregion
    }
}