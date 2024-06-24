using ITFCode.Core.Domain.Tests.EfCoreServices.Base;
using ITFCode.Core.Domain.Tests.TestKit;
using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices;
using ITFCode.Core.Infrastructure.EfCoreServices.Crud;
using ITFCode.Core.Infrastructure.EfCoreServices.Intrefaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ITFCode.Core.Domain.Tests.EfCoreServices
{
    //public class EntityCrudCompositeKey2Service_Tests : EntityCrud_Tests
    //{
    //    #region Reading

    //    [Fact]
    //    public override void Hierarchy_Test()
    //    {
    //        var dbContext = DbContextCreator.CreateClear();

    //        var mockUserService = new Mock<EntityCrudService<UserRoleTc, int, int, TestDbContext>>(dbContext);
    //        Assert.IsAssignableFrom<IEntityCrudService<UserRoleTc, int, int>>(mockUserService.Object);
    //    }

    //    [Theory]
    //    [InlineData(EntityState.Detached, true)]
    //    [InlineData(EntityState.Unchanged, false)]
    //    public override void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
    //    {
    //        var dbContext = DbContextCreator.CreateFulled();
    //        var userService = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var user = userService.Get(1, asNoTracking);
    //        Assert.NotNull(user);
    //        Assert.Equal(1, user.Id);
    //        Assert.Equal(entityState, dbContext.Entry(user).State);
    //    }

    //    [Theory]
    //    [InlineData(EntityState.Detached, true)]
    //    [InlineData(EntityState.Unchanged, false)]
    //    public override async Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking)
    //    {
    //        var dbContext = DbContextCreator.CreateFulled();
    //        var userService = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var user = await userService.GetAsync(1, asNoTracking);
    //        Assert.NotNull(user);
    //        Assert.Equal(1, user.Id);
    //        Assert.Equal(entityState, dbContext.Entry(user).State);
    //    }

    //    [Fact]
    //    public override async Task GetAsync_Throw_If_Cancellation_Requested()
    //    {
    //        var dbContext = DbContextCreator.CreateFulled();
    //        var service = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var cancellationTokenSource = new CancellationTokenSource();
    //        var cancellationToken = cancellationTokenSource.Token;
    //        cancellationTokenSource.Cancel(true);

    //        await Assert.ThrowsAsync<OperationCanceledException>(() => service.GetAsync(DefaultData.UserUser.Key, cancellationToken: cancellationToken));
    //    }

    //    #endregion

    //    #region Creating

    //    [Fact]
    //    public override async Task InsertAsync_Throw_If_Cancellation_Requested()
    //    {
    //        var dbContext = DbContextCreator.CreateClear();
    //        var service = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var cancellationTokenSource = new CancellationTokenSource();
    //        var cancellationToken = cancellationTokenSource.Token;
    //        cancellationTokenSource.Cancel(true);

    //        await Assert.ThrowsAsync<OperationCanceledException>(() => service.InsertAsync(DefaultData.UserUser, cancellationToken: cancellationToken));
    //    }

    //    #endregion

    //    #region Updating

    //    #endregion

    //    #region Removing

    //    [Fact]
    //    public override async Task DeleteAsync_By_Key_Throw_If_Cancellation_Requested()
    //    {
    //        var dbContext = DbContextCreator.CreateFulled();
    //        var service = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var entity = await dbContext.Users.FirstAsync();

    //        var cancellationTokenSource = new CancellationTokenSource();
    //        var cancellationToken = cancellationTokenSource.Token;
    //        cancellationTokenSource.Cancel(true);

    //        await Assert.ThrowsAsync<OperationCanceledException>(() => service.DeleteAsync(entity.Key, cancellationToken: cancellationToken));
    //    }

    //    [Fact]
    //    public override async Task DeleteAsync_By_Entity_Throw_If_Cancellation_Requested()
    //    {
    //        var dbContext = DbContextCreator.CreateFulled();
    //        var service = new EntityCrudService<UserTc, int, TestDbContext>(dbContext);

    //        var entity = await dbContext.Users.FirstAsync();

    //        var cancellationTokenSource = new CancellationTokenSource();
    //        var cancellationToken = cancellationTokenSource.Token;
    //        cancellationTokenSource.Cancel(true);

    //        await Assert.ThrowsAsync<OperationCanceledException>(() => service.DeleteAsync(entity.Key, cancellationToken: cancellationToken));
    //    }

    //    public override Task InsertRangeAsync_Throw_If_Cancellation_Requested()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task UpdateAsync_Throw_If_Cancellation_Requested()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task UpdateRangeAsync_Throw_If_Cancellation_Requested()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}
}