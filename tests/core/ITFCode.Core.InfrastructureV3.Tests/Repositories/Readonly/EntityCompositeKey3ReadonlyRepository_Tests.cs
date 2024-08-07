﻿using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntityCompositeKey3ReadonlyRepositoryTests : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get, GetAsync, GetMany & GetManyAsync

        [Fact] // Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)
        public override void Get_Ok()
        {
            AddTestingData();

            (Guid, string, int) key = (DefaultData.ProductOrder1.CompanyId, DefaultData.ProductOrder1.LocationId, DefaultData.ProductOrder1.UserId);
            var repository = CreateRepository();
            var entity = repository.Get(key);

            Assert.NotNull(entity);
            Assert.Equal(key.Item1, entity.Key1);
            Assert.Equal(key.Item2, entity.Key2);
            Assert.Equal(key.Item3, entity.Key3);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
        }

        [Fact] // GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetAsync_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key = (DefaultData.ProductOrder1.CompanyId, DefaultData.ProductOrder1.LocationId, DefaultData.ProductOrder1.UserId);
            var repository = CreateRepository();
            var entity = await repository.GetAsync(key);

            Assert.NotNull(entity);
            Assert.Equal(key.Item1, entity.Key1);
            Assert.Equal(key.Item2, entity.Key2);
            Assert.Equal(key.Item3, entity.Key3);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(entity).State);
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

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.CompanyId, DefaultData.ProductOrder1.LocationId, DefaultData.ProductOrder1.UserId);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.CompanyId, DefaultData.ProductOrder2.LocationId, DefaultData.ProductOrder2.UserId);

            var repository = CreateRepository();
            IEnumerable<(Guid, string, int)> keys = [key1, key2];

            var entities = repository.GetRange(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2 && u.Key3 == k.Item3) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetRangeAsync_Ok()
        {
            await AddTestingDataAsync();

            (Guid, string, int) key1 = (DefaultData.ProductOrder1.CompanyId, DefaultData.ProductOrder1.LocationId, DefaultData.ProductOrder1.UserId);
            (Guid, string, int) key2 = (DefaultData.ProductOrder2.CompanyId, DefaultData.ProductOrder2.LocationId, DefaultData.ProductOrder2.UserId);

            var repository = CreateRepository();
            IEnumerable<(Guid, string, int)> keys = [key1, key2];

            var entities = await repository.GetRangeAsync(keys);

            Assert.NotEmpty(entities);
            Assert.Equal(2, entities.Count);
            Assert.True(entities.All(u => EntityState.Detached == _dbContext.Entry(u).State));
            Assert.True(keys.All(k => entities.SingleOrDefault(u => u.Key1 == k.Item1 && u.Key2 == k.Item2 && u.Key3 == k.Item3) is not null));
        }

        [Fact] // GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<(Guid, string, int)> keys = [
                (DefaultData.ProductOrder1.Key1, DefaultData.ProductOrder1.Key2, DefaultData.ProductOrder1.Key3),
                (DefaultData.ProductOrder2.Key1, DefaultData.ProductOrder2.Key2, DefaultData.ProductOrder2.Key3)];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetRangeAsync(keys, cancellationToken: cancellationToken));
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