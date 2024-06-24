using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.Domain.Exceptions.Collections;
using ITFCode.Core.InfrastructureV3.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.InfrastructureV3.Tests.EfCore
{
    public class EfEntityCreater_Tests : EfEntityBaseOperator_Tests
    {
        #region Tests: Constructors

        [Fact]
        public void Constructor_If_Param_Is_Null_Then_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new EfEntityCreater<TestDbContext>(null));
        }

        #endregion

        #region Tests: Insert(TEntity entity, bool shouldSave = false)

        [Fact]
        public void Insert_If_Param_ShouldSave_Is_False_Then_Not_Saved()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == user.Id));
            var entity = creator.Insert(user, false);

            Assert.NotNull(entity);
            Assert.Equal(EntityState.Added, _dbContext.Users.Entry(entity).State);

            Assert.False(_dbContext.Users.Any(x => x.Id == user.Id));
        }

        [Fact]
        public void Insert_If_Param_ShouldSave_Is_True_Then_Saved()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == user.Id));
            var entity = creator.Insert(user, true);

            Assert.NotNull(entity);
            Assert.Equal(EntityState.Unchanged, _dbContext.Users.Entry(entity).State);

            Assert.True(_dbContext.Users.Any(x => x.Id == user.Id));
        }

        #endregion

        #region Tests: Task<TEntity> InsertAsync(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Fact]
        public async Task InsertAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id));
            var entity = await creator.InsertAsync(user, false);

            Assert.NotNull(entity);
            Assert.Equal(EntityState.Added, _dbContext.Users.Entry(entity).State);

            Assert.False(await _dbContext.Users.AnyAsync(x => x.Id == user.Id));
        }

        [Fact]
        public async Task InsertAsync_If_Param_ShouldSave_Is_True_Then_Saved()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id));
            var entity = await creator.InsertAsync(user, true);

            Assert.NotNull(entity);
            Assert.Equal(EntityState.Unchanged, _dbContext.Users.Entry(entity).State);

            Assert.True(await _dbContext.Users.AnyAsync(x => x.Id == user.Id));
        }

        [Fact]
        public async Task InsertAsync_Throw_If_Cancellation_Requested()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => creator.InsertAsync(user, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: InsertRange(IEnumerable<TEntity> entities, bool shouldSave = false)

        [Fact]
        public void InsertRange_Throw_If_Range_Is_Null_Or_Empty()
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);
            IEnumerable<UserTc>? users = null;

            Assert.Throws<ArgumentNullException>(() => creator.InsertRange(users));
            Assert.Throws<CollectionIsEmptyException>(() => creator.InsertRange(Enumerable.Empty<UserTc>()));
            users = [userAdmin, null];
            Assert.Throws<CollectionContainsNullException>(() => creator.InsertRange(users));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void InsertRange_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == userAdmin.Id));
            Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == userManager.Id));

            creator.InsertRange([userAdmin, userManager], shouldSave);

            if (shouldSave)
            {
                Assert.NotEqual(EntityState.Added, _dbContext.Users.Entry(userAdmin).State);
                Assert.NotEqual(EntityState.Added, _dbContext.Users.Entry(userManager).State);

                Assert.NotNull(_dbContext.Users.FirstOrDefault(x => x.Id == userAdmin.Id));
                Assert.NotNull(_dbContext.Users.FirstOrDefault(x => x.Id == userManager.Id));
            }
            else
            {
                Assert.Equal(EntityState.Added, _dbContext.Users.Entry(userAdmin).State);
                Assert.Equal(EntityState.Added, _dbContext.Users.Entry(userManager).State);

                Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == userAdmin.Id));
                Assert.Null(_dbContext.Users.FirstOrDefault(x => x.Id == userManager.Id));
            }
        }

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task InsertRangeAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);

            Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userAdmin.Id));
            Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userManager.Id));

            await creator.InsertRangeAsync([userAdmin, userManager], shouldSave);

            if (shouldSave)
            {
                Assert.NotEqual(EntityState.Added, _dbContext.Users.Entry(userAdmin).State);
                Assert.NotEqual(EntityState.Added, _dbContext.Users.Entry(userManager).State);

                Assert.NotNull(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userAdmin.Id));
                Assert.NotNull(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userManager.Id));
            }
            else
            {
                Assert.Equal(EntityState.Added, _dbContext.Users.Entry(userAdmin).State);
                Assert.Equal(EntityState.Added, _dbContext.Users.Entry(userManager).State);

                Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userAdmin.Id));
                Assert.Null(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userManager.Id));
            }
        }

        [Fact]
        public async Task InsertRangeAsync_Throw_If_Cancellation_Requested()
        {
            var user = DefaultData.UserAdmin;
            var creator = new EfEntityCreater<TestDbContext>(_dbContext);
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => creator.InsertRangeAsync([user], cancellationToken: cancellationToken));
        }

        #endregion
    }
}