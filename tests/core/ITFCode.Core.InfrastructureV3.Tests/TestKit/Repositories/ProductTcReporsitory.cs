using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud;

namespace ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories
{
    public class ProductTcReporsitory(TestDbContext dbContext) 
        : EntityRepository<TestDbContext, ProductTc, long, string>(dbContext)
    {
    }
}
