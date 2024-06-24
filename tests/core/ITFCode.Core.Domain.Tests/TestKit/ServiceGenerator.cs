using ITFCode.Core.Domain.Tests.TestKit.Services;

namespace ITFCode.Core.Domain.Tests.TestKit
{
    internal static class ServiceGenerator
    {
        public static UserTcCrudService CreateUserService(TestDbContext? dbContext = default)
            => new(dbContext ?? DbContextCreator.CreateClear());
        public static RoleTcCrudService CreateRoleService(TestDbContext? dbContext = default)
            => new(dbContext ?? DbContextCreator.CreateClear());
        public static LocationTcCrudService CreateLocationService(TestDbContext? dbContext = default)
            => new(dbContext ?? DbContextCreator.CreateClear());
        public static CompanyTcCrudService CreateCompanyService(TestDbContext? dbContext = default)
            => new(dbContext ?? DbContextCreator.CreateClear());
        public static ProductTcCrudService CreateProductService(TestDbContext? dbContext = default)
            => new(dbContext ?? DbContextCreator.CreateClear());
    }
}
