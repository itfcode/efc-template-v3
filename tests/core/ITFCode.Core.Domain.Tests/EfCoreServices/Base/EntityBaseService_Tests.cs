using ITFCode.Core.Domain.Tests.TestKit;

namespace ITFCode.Core.Domain.Tests.EfCoreServices.Base
{
    public abstract class EntityBaseService_Tests<TService> where TService : class
    {
        #region Private & Protected Fields 

        protected readonly TestDbContext _dbContext;
        protected readonly TService _testService;

        #endregion

        #region Constructors

        protected EntityBaseService_Tests()
        {
            _dbContext = DbContextCreator.CreateFulled();
            _testService = CreateService(_dbContext);
        }

        #endregion

        #region Protected Methods 

        protected CancellationToken CreateCancellationToken(bool throwOnFirstException = true) 
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            cancellationTokenSource.Cancel(throwOnFirstException);

            return cancellationToken;
        }

        protected abstract TService CreateService(TestDbContext dbContext);

        #endregion
    }
}