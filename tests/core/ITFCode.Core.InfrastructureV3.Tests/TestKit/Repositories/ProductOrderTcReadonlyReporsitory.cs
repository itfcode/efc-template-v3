using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly;

namespace ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories
{
    public class ProductOrderTcReadonlyReporsitory(TestDbContext dbContext)
        : EntityReadonlyRepository<TestDbContext, ProductOrderTc, Guid, string, int>(dbContext)
    {
    }
}
