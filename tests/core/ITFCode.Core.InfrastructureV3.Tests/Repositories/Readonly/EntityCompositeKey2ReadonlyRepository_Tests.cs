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
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            AddEnities(productA, productB);

            var repository = CreateRepository();
            var product = repository.Get((productA.Id, productA.CountryCode));

            Assert.NotNull(product);
            Assert.Equal(productA.Id, product.Key1);
            Assert.Equal(productA.CountryCode, product.Key2);

            var state = _dbContext.Entry(product).State;

            Assert.Equal(EntityState.Detached, state);
        }

        #endregion

        #region Tests: GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            AddEnities(productA, productB);

            var repository = CreateRepository();
            var product = await repository.GetAsync((productA.Id, productA.CountryCode));

            Assert.NotNull(product);
            Assert.Equal(productA.Id, product.Key1);
            Assert.Equal(productA.CountryCode, product.Key2);

            var state = _dbContext.Entry(product).State;

            Assert.Equal(EntityState.Detached, state);
        }

        #endregion

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2)> keys )

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
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