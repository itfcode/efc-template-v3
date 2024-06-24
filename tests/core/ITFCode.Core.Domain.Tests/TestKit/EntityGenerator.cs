using AutoFixture;
using ITFCode.Core.Domain.Tests.TestKit.Entities;

namespace ITFCode.Core.Domain.Tests.TestKit
{
    internal static class EntityGenerator
    {
        #region Private Fields

        private static readonly Fixture _fixture = new();

        #endregion

        #region Public Method 

        public static List<UserRoleTc> CreateDefaultUserRoles1()
        {
            List<UserRoleTc> list = [];
            var roles = DefaultData.Roles;

            foreach (var user in DefaultData.Users)
            {
                var length = roles.Count;
                ICollection<int> indexes = Enumerable.Range(0, 2)
                    .Select(x => new Random().Next(1, length))
                    .Distinct()
                    .ToList();

                foreach (var idx in indexes)
                {
                    var role = roles[idx];
                    list.Add(new UserRoleTc { UserId = user.Id, RoleId = role.Id });
                }
            }

            return list;
        }

        public static List<UserTc> CreateUsers(int start, int count)
        {
            return Enumerable.Range(start, count)
                .Select(x => CreateUser(x))
                .ToList();
        }

        public static List<RoleTc> CreateRoles(int start, int count)
        {
            return Enumerable.Range(start, count)
                .Select(x => new RoleTc
                {
                    Id = x,
                    Name = _fixture.Create<string>()
                })
                .ToList();
        }

        public static List<UserRoleTc> CreateUserRoles(IEnumerable<UserTc> users, IEnumerable<RoleTc> roles)
        {
            List<UserRoleTc> userRoles = [];

            foreach (var user in users)
            {
                foreach (var role in roles)
                {
                    userRoles.Add(new UserRoleTc
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                }
            }

            return userRoles;
        }

        public static UserTc CreateUser(int id, string? firstName = null, string? lastName = null)
            => new()
            {
                Id = id,
                FirstName = firstName ?? Faker.Name.First(),
                LastName = lastName ?? Faker.Name.Last()
            };

        public static RoleTc CreateRole(int id, string name)
            => new()
            {
                Id = id,
                Name = name,
            };

        public static UserRoleTc CreateUserRole(int userId, int roleId)
            => new()
            {
                UserId = userId,
                RoleId = roleId
            };

        public static LocationTc CreateLocation(string id, string name)
            => new()
            {
                Id = id,
                Name = name,
            };

        public static CompanyTc CreateCompany(Guid companyId, string companyName)
            => new()
            {
                Id = companyId,
                Name = companyName
            };

        public static ProductOrderTc CreateProductOrder(Guid companyId, string locationId, int userId)
            => new()
            {
                CompanyId = companyId,
                LocationId = locationId,
                UserId = userId,
                Code = Faker.RandomNumber.Next(1, 3000)
            };

        #endregion
    }
}