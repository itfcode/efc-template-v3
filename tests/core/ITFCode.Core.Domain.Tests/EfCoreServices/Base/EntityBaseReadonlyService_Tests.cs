using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ITFCode.Core.Infrastructure.EfCoreServices.Crud;

namespace ITFCode.Core.Domain.Tests.EfCoreServices.Base
{
    public abstract class EntityBaseReadonlyService_Tests<TService> : EntityBaseService_Tests<TService>
        where TService : class
    {
        [Theory]
        [InlineData("Get")]
        [InlineData("GetAsync")]
        ///[InlineData("Insert")]
        //[InlineData("InsertAsync")]
        //[InlineData("InsertRange")]
        //[InlineData("InsertRangeAsync")]
        [InlineData("Update")]
        [InlineData("UpdateAsync")]
        [InlineData("UpdateRange")]
        [InlineData("UpdateRangeAsync")]
        public void Method_Should_Be_Virtual(string methodName)
        {
            var methods = typeof(EntityCrudBaseService<,>).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == methodName)
                .Union(typeof(EntityCrudService<,,>).GetMethods(BindingFlags.Public)
                .Where(x => x.Name == methodName))
                .Union(typeof(EntityCrudService<,,,>).GetMethods(BindingFlags.Public)
                .Where(x => x.Name == methodName))
                .Union(typeof(EntityCrudService<,,,,>).GetMethods(BindingFlags.Public)
                .Where(x => x.Name == methodName))
                .ToList();

            Assert.NotEmpty(methods);

            foreach (var method in methods)
            {
                Assert.True(method.IsVirtual);
            }
        }

        public abstract void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);
        public abstract Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);
        public abstract Task GetAsync_Throw_If_Cancellation_Requested();

        public abstract void GetMany_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);

        public abstract Task GetManyAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);

        public abstract Task GetManyAsync_Throw_If_Cancellation_Requested();
    }
}