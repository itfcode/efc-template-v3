using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.EfCore
{
    public class EfEntityUpdater_Tests : EfEntityBaseOperator_Tests
    {
        #region Tests: Constructor

        [Fact]
        public void Constructor_If_Param_Is_Null_Then_Throw()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new EfEntityUpdater<TestDbContext>(null));
        }

        #endregion

        #region Tests: Update<TEntity>(TEntity entity, bool shouldBeSaved = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Update_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = UserSet.AsNoTracking().Single(predicate);

            var newFirstName = $"New_{entityBefore.FirstName}";

            entityBefore.FirstName = newFirstName;
            updater.Update(entityBefore, shouldSave);

            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = UserSet.AsNoTracking().Single(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Unchanged, state);
                Assert.Equal(newFirstName, entityAfter.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Modified, state);
                Assert.NotEqual(newFirstName, entityAfter.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateAsync<TEntity>(TEntity entity, bool shouldBeSaved = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            Assert.NotNull(await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = await UserSet.AsNoTracking().SingleAsync(predicate);

            var newFirstName = $"New_{entityBefore.FirstName}";

            entityBefore.FirstName = newFirstName;
            await updater.UpdateAsync(entityBefore, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await UserSet.AsNoTracking().SingleAsync(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Unchanged, state);
                Assert.Equal(newFirstName, entityAfter.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Modified, state);
                Assert.NotEqual(newFirstName, entityAfter.FirstName);
            }
        }

        #endregion

        #region Tests: Update<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = UserSet.AsNoTracking().Single(predicate);

            var newFirstName = $"New_{entityBefore.FirstName}";

            entityBefore.FirstName = newFirstName;

            Action<UserTc> updateStrategy = (x) => x.FirstName = newFirstName;
            updater.Update(entityBefore.Id, updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = UserSet.AsNoTracking().Single(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newFirstName, entityAfter.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newFirstName, entityAfter.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateAsync<TEntity>(object key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsyncSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            Assert.NotNull(await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = await UserSet.AsNoTracking().SingleAsync(predicate);

            var newFirstName = $"New_{entityBefore.FirstName}";

            entityBefore.FirstName = newFirstName;

            Action<UserTc> updateStrategy = (x) => x.FirstName = newFirstName;
            await updater.UpdateAsync(entityBefore.Id, updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await UserSet.AsNoTracking().SingleAsync(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newFirstName, entityAfter.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newFirstName, entityAfter.FirstName);
            }
        }

        #endregion

        #region Tests: Update<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var roleAdmin = DefaultData.RoleAdmin;
            AddEnities(userAdmin, roleAdmin);

            var userAdminRole = EntityGenerator.CreateUserRole(userAdmin.Id, roleAdmin.Id);
            AddEnities(userAdminRole);

            Assert.NotNull(_dbContext.UserRoles.FirstOrDefault(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = UserRoleSet.AsNoTracking().Single(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id);

            var newComment = $"New_{entityBefore.Comment}";

            entityBefore.Comment = newComment;

            Action<UserRoleTc> updateStrategy = (x) => x.Comment = newComment;

            updater.Update((entityBefore.UserId, entityBefore.RoleId), updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = UserRoleSet.AsNoTracking().Single(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newComment, entityAfter.Comment);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newComment, entityAfter.Comment);
            }
        }

        #endregion

        #region Tests: UpdateAsync<TEntity>((object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsyncCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var roleAdmin = DefaultData.RoleAdmin;
            AddEnities(userAdmin, roleAdmin);

            var userAdminRole = EntityGenerator.CreateUserRole(userAdmin.Id, roleAdmin.Id);
            AddEnities(userAdminRole);

            Assert.NotNull(await UserRoleSet.FirstOrDefaultAsync(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = await UserRoleSet.AsNoTracking().SingleAsync(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id);

            var newComment = $"New_{entityBefore.Comment}";

            entityBefore.Comment = newComment;

            Action<UserRoleTc> updateStrategy = (x) => x.Comment = newComment;

            await updater.UpdateAsync((entityBefore.UserId, entityBefore.RoleId), updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await UserRoleSet.AsNoTracking().SingleAsync(x => x.UserId == userAdmin.Id && x.RoleId == roleAdmin.Id);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newComment, entityAfter.Comment);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newComment, entityAfter.Comment);
            }
        }

        #endregion

        #region Tests: Update<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            Guid companyId = Guid.NewGuid();
            string locationId = "101";
            int userId = 10;

            var productOrder = EntityGenerator.CreateProductOrder(companyId, locationId, userId);
            AddEnities(productOrder);

            Expression<Func<ProductOrderTc, bool>> predicate = p => p.CompanyId == companyId && p.LocationId == locationId && p.UserId == userId;
            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = ProductOrderSet.AsNoTracking().Single(predicate);

            var newCode = entityBefore.Code + 1000;

            Action<ProductOrderTc> updateStrategy = (x) => x.Code = newCode;

            updater.Update((entityBefore.CompanyId, entityBefore.LocationId, entityBefore.UserId), updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = ProductOrderSet.AsNoTracking().Single(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newCode, entityAfter.Code);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newCode, entityAfter.Code);
            }
        }

        #endregion

        #region Tests: UpdateAsync<TEntity>((object, object, object) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateAsyncCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            Guid companyId = Guid.NewGuid();
            string locationId = "101";
            int userId = 10;

            var productOrder = EntityGenerator.CreateProductOrder(companyId, locationId, userId);
            AddEnities(productOrder);

            Expression<Func<ProductOrderTc, bool>> predicate = p => p.CompanyId == companyId && p.LocationId == locationId && p.UserId == userId;
            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore = await ProductOrderSet.AsNoTracking().SingleAsync(predicate);

            var newCode = entityBefore.Code + 1000;

            Action<ProductOrderTc> updateStrategy = (x) => x.Code = newCode;

            await updater.UpdateAsync((entityBefore.CompanyId, entityBefore.LocationId, entityBefore.UserId), updateStrategy, shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await ProductOrderSet.AsNoTracking().SingleAsync(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Equal(newCode, entityAfter.Code);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotEqual(newCode, entityAfter.Code);
            }
        }

        #endregion

        #region Tests: UpdateRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateRange_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(x => x.Id == userAdmin.Id));
            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(x => x.Id == userManager.Id));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = UserSet.AsNoTracking().Single(x => x.Id == userAdmin.Id);
            var entityBefore2 = UserSet.AsNoTracking().Single(x => x.Id == userManager.Id);

            var newFirstName1 = $"New_{entityBefore1.FirstName}";
            var newFirstName2 = $"New_{entityBefore2.FirstName}";

            Assert.NotEqual(newFirstName1, entityBefore1.FirstName);
            Assert.NotEqual(newFirstName2, entityBefore2.FirstName);

            entityBefore1.FirstName = newFirstName1;
            entityBefore2.FirstName = newFirstName2;

            updater.UpdateRange([entityBefore1, entityBefore2], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = UserSet.AsNoTracking().Single(x => x.Id == userAdmin.Id);
            var entityAfter2 = UserSet.AsNoTracking().Single(x => x.Id == userManager.Id);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Unchanged, state1);
                Assert.Equal(EntityState.Unchanged, state2);
                Assert.Equal(newFirstName1, entityAfter1.FirstName);
                Assert.Equal(newFirstName2, entityAfter2.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Modified, state1);
                Assert.Equal(EntityState.Modified, state2);
                Assert.NotEqual(newFirstName1, entityAfter1.FirstName);
                Assert.NotEqual(newFirstName2, entityAfter2.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateRangeAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(x => x.Id == userAdmin.Id));
            Assert.NotNull(UserSet.AsNoTracking().FirstOrDefault(x => x.Id == userManager.Id));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userAdmin.Id);
            var entityBefore2 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userManager.Id);

            var newFirstName1 = $"New_{entityBefore1.FirstName}";
            var newFirstName2 = $"New_{entityBefore2.FirstName}";
            Assert.NotEqual(newFirstName1, entityBefore1.FirstName);
            Assert.NotEqual(newFirstName2, entityBefore2.FirstName);

            entityBefore1.FirstName = newFirstName1;
            entityBefore2.FirstName = newFirstName2;

            await updater.UpdateRangeAsync([entityBefore1, entityBefore2], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userAdmin.Id);
            var entityAfter2 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userManager.Id);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Unchanged, state1);
                Assert.Equal(EntityState.Unchanged, state2);
                Assert.Equal(newFirstName1, entityAfter1.FirstName);
                Assert.Equal(newFirstName2, entityAfter2.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Modified, state1);
                Assert.Equal(EntityState.Modified, state2);
                Assert.NotEqual(newFirstName1, entityAfter1.FirstName);
                Assert.NotEqual(newFirstName2, entityAfter2.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateRange<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateRangeSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            var entityBefore1 = UserSet.AsNoTracking().Single(x => x.Id == userAdmin.Id);
            var entityBefore2 = UserSet.AsNoTracking().Single(x => x.Id == userManager.Id);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);

            var newFirstName = $"New_FirstName";

            Action<UserTc> updateStrategy = (x) => x.FirstName = newFirstName;
            updater.UpdateRange([userAdmin.Id, userManager.Id], updateStrategy, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = UserSet.AsNoTracking().Single(x => x.Id == userAdmin.Id);
            var entityAfter2 = UserSet.AsNoTracking().Single(x => x.Id == userManager.Id);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newFirstName, entityAfter1.FirstName);
                Assert.Equal(newFirstName, entityAfter2.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newFirstName, entityAfter1.FirstName);
                Assert.NotEqual(newFirstName, entityAfter2.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateRangeAsync<TEntity>(IEnumerable<object> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateRangeAsyncSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            AddEnities(userAdmin, userManager);

            var entityBefore1 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userAdmin.Id);
            var entityBefore2 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userManager.Id);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);

            var newFirstName = $"New_FirstName";

            Action<UserTc> updateStrategy = (x) => x.FirstName = newFirstName;

            await updater.UpdateRangeAsync([userAdmin.Id, userManager.Id], updateStrategy, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userAdmin.Id);
            var entityAfter2 = await UserSet.AsNoTracking().SingleAsync(x => x.Id == userManager.Id);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newFirstName, entityAfter1.FirstName);
                Assert.Equal(newFirstName, entityAfter2.FirstName);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newFirstName, entityAfter1.FirstName);
                Assert.NotEqual(newFirstName, entityAfter2.FirstName);
            }
        }

        #endregion

        #region Tests: UpdateRange<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateRangeCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            AddEnities(productA, productB);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == productA.Id && x.CountryCode == productA.CountryCode;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == productA.Id && x.CountryCode == productA.CountryCode;

            Assert.NotNull(ProductSet.FirstOrDefault(predicate1));
            Assert.NotNull(ProductSet.FirstOrDefault(predicate2));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = ProductSet.AsNoTracking().Single(predicate1);
            var entityBefore2 = ProductSet.AsNoTracking().Single(predicate2);

            var newName = $"New_ProductName";

            Assert.NotEqual(newName, entityBefore1.Name);
            Assert.NotEqual(newName, entityBefore2.Name);

            Action<ProductTc> updateStrategy = (x) => x.Name = newName;

            IEnumerable<(object, object)> keys = [
                (entityBefore1.Id, entityBefore1.CountryCode),
                (entityBefore2.Id, entityBefore2.CountryCode)];

            updater.UpdateRange(keys, updateStrategy, shouldSave);
            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = ProductSet.AsNoTracking().Single(predicate1);
            var entityAfter2 = ProductSet.AsNoTracking().Single(predicate2);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newName, entityAfter1.Name);
                Assert.Equal(newName, entityAfter2.Name);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newName, entityAfter1.Name);
                Assert.NotEqual(newName, entityAfter2.Name);
            }
        }

        #endregion

        #region Tests: UpdateRangeAsync<TEntity>(IEnumerable<(object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateRangeAsyncCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            AddEnities(productA, productB);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == productA.Id && x.CountryCode == productA.CountryCode;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == productA.Id && x.CountryCode == productA.CountryCode;

            Assert.NotNull(await ProductSet.FirstOrDefaultAsync(predicate1));
            Assert.NotNull(await ProductSet.FirstOrDefaultAsync(predicate2));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = await ProductSet.AsNoTracking().SingleAsync(predicate1);
            var entityBefore2 = await ProductSet.AsNoTracking().SingleAsync(predicate2);

            var newName = $"New_ProductName";

            Assert.NotEqual(newName, entityBefore1.Name);
            Assert.NotEqual(newName, entityBefore2.Name);

            Action<ProductTc> updateStrategy = (x) => x.Name = newName;

            IEnumerable<(object, object)> keys = [
                (entityBefore1.Id, entityBefore1.CountryCode),
                (entityBefore2.Id, entityBefore2.CountryCode)];

            await updater.UpdateRangeAsync(keys, updateStrategy, shouldSave);
            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await ProductSet.AsNoTracking().SingleAsync(predicate1);
            var entityAfter2 = await ProductSet.AsNoTracking().SingleAsync(predicate2);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newName, entityAfter1.Name);
                Assert.Equal(newName, entityAfter2.Name);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newName, entityAfter1.Name);
                Assert.NotEqual(newName, entityAfter2.Name);
            }
        }

        #endregion

        #region Tests: UpdateRange<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateRangeCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrder1 = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            var productOrder2 = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "102", 2);
            AddEnities(productOrder1, productOrder2);

            Expression<Func<ProductOrderTc, bool>> predicate1 = p => p.CompanyId == productOrder1.CompanyId && p.LocationId == productOrder1.LocationId
                && p.UserId == productOrder1.UserId;
            Expression<Func<ProductOrderTc, bool>> predicate2 = p => p.CompanyId == productOrder2.CompanyId && p.LocationId == productOrder2.LocationId
                && p.UserId == productOrder2.UserId;

            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate1));
            Assert.NotNull(ProductOrderSet.FirstOrDefault(predicate2));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = ProductOrderSet.AsNoTracking().Single(predicate1);
            var entityBefore2 = ProductOrderSet.AsNoTracking().Single(predicate2);

            var newCode = (entityBefore1.Code + entityBefore2.Code) + 1000;

            Action<ProductOrderTc> updateStrategy = (x) => x.Code = newCode;

            IEnumerable<(object, object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2, entityBefore1.Key3),
                (entityBefore2.Key1, entityBefore2.Key2, entityBefore2.Key3)];

            updater.UpdateRange(keys, updateStrategy, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = ProductOrderSet.AsNoTracking().Single(predicate1);
            var entityAfter2 = ProductOrderSet.AsNoTracking().Single(predicate2);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newCode, entityAfter1.Code);
                Assert.Equal(newCode, entityAfter2.Code);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newCode, entityAfter1.Code);
                Assert.NotEqual(newCode, entityAfter2.Code);
            }
        }

        #endregion

        #region Tests: UpdateRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task UpdateRangeAsyncCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrder1 = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "101", 1);
            var productOrder2 = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "102", 2);
            AddEnities(productOrder1, productOrder2);

            Expression<Func<ProductOrderTc, bool>> predicate1 = p => p.CompanyId == productOrder1.CompanyId && p.LocationId == productOrder1.LocationId
                && p.UserId == productOrder1.UserId;
            Expression<Func<ProductOrderTc, bool>> predicate2 = p => p.CompanyId == productOrder2.CompanyId && p.LocationId == productOrder2.LocationId
                && p.UserId == productOrder2.UserId;

            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate1));
            Assert.NotNull(await ProductOrderSet.FirstOrDefaultAsync(predicate2));

            var updater = new EfEntityUpdater<TestDbContext>(_dbContext);
            var entityBefore1 = await ProductOrderSet.AsNoTracking().SingleAsync(predicate1);
            var entityBefore2 = await ProductOrderSet.AsNoTracking().SingleAsync(predicate2);

            var newCode = (entityBefore1.Code + entityBefore2.Code) + 1000;

            Action<ProductOrderTc> updateStrategy = (x) => x.Code = newCode;

            IEnumerable<(object, object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2, entityBefore1.Key3),
                (entityBefore2.Key1, entityBefore2.Key2, entityBefore2.Key3)];

            await updater.UpdateRangeAsync(keys, updateStrategy, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await ProductOrderSet.AsNoTracking().SingleAsync(predicate1);
            var entityAfter2 = await ProductOrderSet.AsNoTracking().SingleAsync(predicate2);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Equal(newCode, entityAfter1.Code);
                Assert.Equal(newCode, entityAfter2.Code);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.NotEqual(newCode, entityAfter1.Code);
                Assert.NotEqual(newCode, entityAfter2.Code);
            }
        }

        #endregion
    }
}