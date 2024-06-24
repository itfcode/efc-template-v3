using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.EfCoreServices.Base
{
    public abstract class EntityBaseCrudService_Tests<TService> : EntityBaseService_Tests<TService>
        where TService : class
    {
        #region Private & Protected Fields 

        #endregion

        #region Constructors

        protected EntityBaseCrudService_Tests() : base() { }

        #endregion

        #region Mandatory Test Methods 

        public abstract void Get_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);

        public abstract Task GetAsync_If_Correct_Param_Then_Ok(EntityState entityState, bool asNoTracking);

        public abstract Task GetAsync_Throw_If_Cancellation_Requested();

        public abstract Task InsertAsync_Throw_If_Cancellation_Requested();
        public abstract Task InsertRangeAsync_Throw_If_Cancellation_Requested();

        public abstract Task UpdateAsync_Throw_If_Cancellation_Requested();
        public abstract Task UpdateAsync_If_Params_Is_Correct_Then_Ok(bool shouldSave);
        
        public abstract Task UpdateRangeAsync_Throw_If_Cancellation_Requested();
        public abstract Task UpdateRangeAsync_If_Params_Is_Correct_Then_Ok(bool shouldSave);

        public abstract Task DeleteAsync_By_Key_Throw_If_Cancellation_Requested();
        public abstract Task DeleteAsync_By_Entity_Throw_If_Cancellation_Requested();

        public abstract Task DeleteRangeAsync_By_Key_Throw_If_Cancellation_Requested();
        public abstract Task DeleteRangeAsync_By_Entity_Throw_If_Cancellation_Requested();

        #endregion

        //[Theory]
        //[InlineData("Delete")]
        //[InlineData("DeleteAsync")]
        //[InlineData("DeleteRange")]
        //[InlineData("DeleteRangeAsync")]
        //public void Method_Should_Be_Virtual(string methodName) 
        //{
        //    var methods = typeof(T).GetMethods()
        //        .Where(x => x.Name == methodName)
        //        .ToList();

        //    Assert.NotEmpty(methods);

        //    foreach (var method in methods)
        //    {
        //        Assert.True(method.IsVirtual);
        //    }
        //}        //[Theory]
        //[InlineData("Delete")]
        //[InlineData("DeleteAsync")]
        //[InlineData("DeleteRange")]
        //[InlineData("DeleteRangeAsync")]
        //public void Method_Should_Be_Virtual(string methodName) 
        //{
        //    var methods = typeof(T).GetMethods()
        //        .Where(x => x.Name == methodName)
        //        .ToList();

        //    Assert.NotEmpty(methods);

        //    foreach (var method in methods)
        //    {
        //        Assert.True(method.IsVirtual);
        //    }
        //}
    }
}
