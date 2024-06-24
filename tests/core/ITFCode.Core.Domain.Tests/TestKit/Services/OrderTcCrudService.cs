using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.EfCoreServices.Crud;

namespace ITFCode.Core.Domain.Tests.TestKit.Services
{
    internal class OrderTcCrudService(TestDbContext dbContext) : EntityCrudBaseService<TestDbContext, ProductTc>(dbContext)
    {
    }
}
