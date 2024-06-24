using ITFCode.Core.Domain.Tests.TestKit.Entities;

namespace ITFCode.Core.Domain.Tests.TestKit
{

    public class DefaultData
    {
        public static UserTc UserAdmin => EntityGenerator.CreateUser(1);
        public static UserTc UserManager => EntityGenerator.CreateUser(2);
        public static UserTc UserUser => EntityGenerator.CreateUser(3);
        public static UserTc UserModerator => EntityGenerator.CreateUser(4);
        public static UserTc UserGuest => EntityGenerator.CreateUser(5);

        public static RoleTc RoleAdmin => EntityGenerator.CreateRole(1, "Admin");
        public static RoleTc RoleManager => EntityGenerator.CreateRole(2, "Manager");
        public static RoleTc RoleUser => EntityGenerator.CreateRole(3, "User");
        public static RoleTc RoleModerator => EntityGenerator.CreateRole(4, "Moderator");
        public static RoleTc RoleGuest => EntityGenerator.CreateRole(5, "Guest");

        public static LocationTc LocationEurope => EntityGenerator.CreateLocation("eu", "Europe");
        public static LocationTc LocationAfrica => EntityGenerator.CreateLocation("af", "Africa");

        public static CompanyTc CompanySun => EntityGenerator.CreateCompany(Guid.Parse("CCD31D2A-28F5-4378-B32B-E297DB58C500"), "Sun Enterprise");
        public static CompanyTc CompanyMoon => EntityGenerator.CreateCompany(Guid.Parse("E9AE831C-8909-402A-AA0F-EBB252390975"), "Moon Inc");

        public static ProductOrderTc ProductOrder1 => EntityGenerator.CreateProductOrder(CompanySun.Id, LocationEurope.Id, UserManager.Id);
        public static ProductOrderTc ProductOrder2 => EntityGenerator.CreateProductOrder(CompanyMoon.Id, LocationAfrica.Id, UserGuest.Id);

        public static IReadOnlyList<UserTc> Users => [UserAdmin, UserManager, UserUser, UserModerator, UserGuest];
        public static IReadOnlyList<RoleTc> Roles => [RoleAdmin, RoleManager, RoleUser, RoleModerator, RoleGuest];
        public static IReadOnlyList<UserRoleTc> UserRoles => [
                EntityGenerator.CreateUserRole(UserAdmin.Id, RoleAdmin.Id),
                EntityGenerator.CreateUserRole(UserManager.Id, RoleManager.Id),
                EntityGenerator.CreateUserRole(UserUser.Id, RoleUser.Id),
                EntityGenerator.CreateUserRole(UserModerator.Id, RoleModerator.Id),
                EntityGenerator.CreateUserRole(UserGuest.Id, RoleUser.Id)];
        public static IReadOnlyList<LocationTc> Locations => [LocationEurope, LocationAfrica];
        public static IReadOnlyList<CompanyTc> Companies => [CompanySun, CompanyMoon];
        public static IReadOnlyList<ProductOrderTc> ProductOrders => [ProductOrder1, ProductOrder2];
    }
}