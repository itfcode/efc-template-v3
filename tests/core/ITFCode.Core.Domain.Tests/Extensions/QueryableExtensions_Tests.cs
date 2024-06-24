using ITFCode.Core.Domain.Tests.TestKit.Entities;
using ITFCode.Core.Infrastructure.Extensions;

namespace ITFCode.Core.Domain.Tests.Extensions
{
    public class QueryableExtensions_Tests
    {
        [Theory]
        [InlineData(10), InlineData(100), InlineData(500)]
        public void MultiFilterByKey_1_Property(int count)
        {
            var users = Enumerable.Range(1, count)
                .Select(x => new UserTc
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Id = x,
                })
                .ToList();
            Assert.Equal(count, users.Count);

            var ids = Enumerable.Range(1, count - 5).ToList();

            var filtered = users.AsQueryable()
                .MultiFilterByKey(ids, nameof(UserTc.Id))
                .ToList();

            Assert.Equal(ids.Count, filtered.Count);
            foreach (var user in filtered)
            {
                Assert.Contains(user.Id, ids);
            }
        }

        [Theory]
        [InlineData(10), InlineData(100), InlineData(500)]
        public void MultiFilterByKey_2_Properties(int count)
        {
            var users = Enumerable.Range(1, count)
                .Select(x => new UserRoleTc
                {
                    RoleId = x,
                    UserId = x,
                })
                .ToList();
            Assert.Equal(count, users.Count);

            var ids = Enumerable.Range(1, count - 5).Select(x => (x, x)).ToList();

            var filtered = users.AsQueryable()
                .MultiFilterByKey(ids, [nameof(UserRoleTc.UserId), nameof(UserRoleTc.RoleId)])
                .ToList();

            Assert.Equal(ids.Count, filtered.Count);
            foreach (var userRole in filtered)
            {
                Assert.Contains((userRole.Key1, userRole.Key2), ids);
            }
        }

        [Theory]
        [InlineData(10), InlineData(100), InlineData(500)]
        public void MultiFilterByKey_3_Properties(int count)
        {
            var orders = Enumerable.Range(1, count)
                .Select(x => new ProductOrderTc
                {
                    CompanyId = Guid.NewGuid(),
                    LocationId = $"{x}",
                    UserId = x
                })
                .ToList();

            Assert.Equal(count, orders.Count);

            var ids = orders.Take(count - 5).Select(x => (x.Key1, x.Key2, x.Key3)).ToList();

            var filtered = orders.AsQueryable()
                .MultiFilterByKey(ids, [nameof(ProductOrderTc.CompanyId), nameof(ProductOrderTc.LocationId), nameof(ProductOrderTc.UserId)])
                .ToList();

            Assert.Equal(ids.Count, filtered.Count);
            foreach (var order in filtered)
            {
                Assert.Contains((order.Key1, order.Key2, order.Key3), ids);
            }
        }
    }
}