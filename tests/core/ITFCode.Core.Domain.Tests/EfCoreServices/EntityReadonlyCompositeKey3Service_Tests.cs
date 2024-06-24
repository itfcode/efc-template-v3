using ITFCode.Core.Domain.Tests.EfCoreServices.Base;
using ITFCode.Core.Domain.Tests.TestKit;
using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using ITFCode.Core.Infrastructure.EfCoreServices.Readonly;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.EfCoreServices
{
    public class EntityReadonlyCompositeKey3Service_Tests 
        : EntityBaseReadonlyService_Tests<IEntityReadonlyService<ProductOrderTc, Guid, string, int>>
    {
        #region Tests

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var companyId = DefaultData.ProductOrder1.CompanyId;
            var locationId = DefaultData.ProductOrder1.LocationId;
            var userId = DefaultData.ProductOrder1.UserId;

            var productOrder = _testService.Get((companyId, locationId, userId), asNoTracking);
            Assert.NotNull(productOrder);

            Assert.Equal(companyId, productOrder.Key1);
            Assert.Equal(locationId, productOrder.Key2);
            Assert.Equal(userId, productOrder.Key3);
            Assert.Equal(entityState, _dbContext.Entry(productOrder).State);

            productOrder = _testService.Get(companyId, locationId, userId, asNoTracking);
            Assert.NotNull(productOrder);
            Assert.Equal(companyId, productOrder.Key1);
            Assert.Equal(locationId, productOrder.Key2);
            Assert.Equal(userId, productOrder.Key3);
            Assert.Equal(entityState, _dbContext.Entry(productOrder).State);
        }

        [Theory]
        [InlineData(EntityState.Detached, true)]
        [InlineData(EntityState.Unchanged, false)]
        public override async Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            var companyId = DefaultData.ProductOrder1.CompanyId;
            var locationId = DefaultData.ProductOrder1.LocationId;
            var userId = DefaultData.ProductOrder1.UserId;

            var productOrder = await _testService.GetAsync((companyId, locationId, userId), asNoTracking);
            Assert.NotNull(productOrder);

            Assert.Equal(companyId, productOrder.Key1);
            Assert.Equal(locationId, productOrder.Key2);
            Assert.Equal(userId, productOrder.Key3);
            Assert.Equal(entityState, _dbContext.Entry(productOrder).State);

            productOrder = await _testService.GetAsync(companyId, locationId, userId, asNoTracking);
            Assert.NotNull(productOrder);
            Assert.Equal(companyId, productOrder.Key1);
            Assert.Equal(locationId, productOrder.Key2);
            Assert.Equal(userId, productOrder.Key3);
            Assert.Equal(entityState, _dbContext.Entry(productOrder).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation_Requested()
        {
            var cancellationToken = CreateCancellationToken();

            var companyId = DefaultData.ProductOrder1.CompanyId;
            var locationId = DefaultData.ProductOrder1.LocationId;
            var userId = DefaultData.ProductOrder1.UserId;

            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.GetAsync((companyId, locationId, userId), cancellationToken: cancellationToken));
            await Assert.ThrowsAsync<OperationCanceledException>(() => _testService.GetAsync(companyId, locationId, userId, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private & Protected 

        protected override IEntityReadonlyService<ProductOrderTc, Guid, string, int> CreateService(TestDbContext dbContext)
            => new EntityReadonlyService<TestDbContext, ProductOrderTc, Guid, string, int>(dbContext);

        public override void GetMany_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public override Task GetManyAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public override Task GetManyAsync_Throw_If_Cancellation_Requested()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
