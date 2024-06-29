using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntityCompositeKey2ReadonlyRepository_Tests : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get, GetAsync, GetMany & GetManyAsync 

        [Fact] // Get((TKey1, TKey2) key)
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            (long, string) key = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);

            var repository = CreateRepository();
            var entity = repository.Get(key);

            Assert.NotNull(entity);
            Assert.Equal(key.Item1, entity.Key1);
            Assert.Equal(key.Item2, entity.Key2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        [Fact] // GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            (long, string) key = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);

            var repository = CreateRepository();
            var entity = await repository.GetAsync(key);

            Assert.NotNull(entity);
            Assert.Equal(key.Item1, entity.Key1);
            Assert.Equal(key.Item2, entity.Key2);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        [Fact] // GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync((1, "key_value"), cancellationToken: cancellationToken));
        }
 
        [Fact] // GetMany(IEnumerable<(TKey1, TKey2)> keys )
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            (long, string) key1 = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);
            (long, string) key2 = (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode);

            var repository = CreateRepository();
            IEnumerable<(long, string)> keys = [key1, key2];

            var entities = repository.GetMany(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            (long, string) key1 = (DefaultData.ProductA.Id, DefaultData.ProductA.CountryCode);
            (long, string) key2 = (DefaultData.ProductB.Id, DefaultData.ProductB.CountryCode);

            var repository = CreateRepository();
            IEnumerable<(long, string)> keys = [key1, key2];

            var entities = await repository.GetManyAsync(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)
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