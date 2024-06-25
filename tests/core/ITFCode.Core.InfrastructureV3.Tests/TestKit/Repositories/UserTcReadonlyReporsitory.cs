using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly;

namespace ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories
{
    public class UserTcReadonlyReporsitory(TestDbContext dbContext) 
        : EntityReadonlyRepository<TestDbContext, UserTc, int>(dbContext)
    {
    }
}