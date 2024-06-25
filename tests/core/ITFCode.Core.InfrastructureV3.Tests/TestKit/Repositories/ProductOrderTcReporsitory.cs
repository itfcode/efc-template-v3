using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Crud;

namespace ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories
{
    public class ProductOrderTcReporsitory(TestDbContext dbContext)
        : EntityRepository<TestDbContext, ProductOrderTc, Guid, string, int>(dbContext)
    {
    }
}
