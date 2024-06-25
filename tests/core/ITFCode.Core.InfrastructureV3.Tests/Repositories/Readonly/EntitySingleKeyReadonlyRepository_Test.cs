using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.Repositories.Readonly.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly
{
    public class EntitySingleKeyReadonlyRepository_Test : EntityReadonlyBaseRepository_Tests
    {
        #region Tests: Get(TKey key, bool asNoTracking = true)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            var repository = CreateService();
            var user = repository.Get(userAdmin.Id);

            Assert.NotNull(user);
            Assert.Equal(userAdmin.Id, user.Key);

            var state = _dbContext.Entry(user).State;

            Assert.Equal(EntityState.Detached, state);
        }

        #endregion

        #region Tests: GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            var repository = CreateService();
            var user = await repository.GetAsync(userAdmin.Id);

            Assert.NotNull(user);
            Assert.Equal(userAdmin.Id, user.Key);

            var state = _dbContext.Entry(user).State;

            Assert.Equal(EntityState.Detached, state);
        }

        #endregion

        #region Tests: GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        [Fact]
        public override Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods 

        private IEntityReadonlyRepository<UserTc, int> CreateService()
        {
            return new UserTcReadonlyReporsitory(_dbContext);
        }

        #endregion
    }
}