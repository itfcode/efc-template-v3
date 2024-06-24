using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.InfrastructureV3.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.EfCore
{
    public class EfEntityDeleters_Tests : EfEntityBaseOperator_Tests
    {
        #region Tests: Constructor

        [Fact]
        public void Constructor_If_Param_Is_Null_Then_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => new EfEntityDeleter<TestDbContext>(null));
        }

        #endregion

        #region Tests: Delete<TEntity>(TEntity entity, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Delete_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            var entityBefore = UserSet.AsNoTracking().FirstOrDefault(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.Delete(entityBefore, shouldSave);

            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = UserSet.AsNoTracking().SingleOrDefault(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.Equal(EntityState.Deleted, state);
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: DeleteAsync<TEntity>(TEntity entity, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            var entityBefore = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.Delete(entityBefore, shouldSave);

            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.Equal(EntityState.Deleted, state);
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: Delete<TEntity>(object key, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            var entityBefore = UserSet.AsNoTracking().FirstOrDefault(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.Delete<UserTc>(entityBefore.Id, shouldSave);

            var state = _dbContext.Entry(entityBefore).State;
            Assert.Equal(EntityState.Detached, state);

            var entityAfter = UserSet.AsNoTracking().SingleOrDefault(predicate);
            if (shouldSave)
            {
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: DeleteAsync<TEntity>(object key, bool shouldSave = false, CancellationToken cancellationToken = default) 

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsyncSingleKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            AddEnities(userAdmin);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == userAdmin.Id;

            var entityBefore = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            await deleter.DeleteAsync<UserTc>(entityBefore.Id, shouldSave);

            var state = _dbContext.Entry(entityBefore).State;
            Assert.Equal(EntityState.Detached, state);

            var entityAfter = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate);

            if (shouldSave)
            {
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: Delete<TEntity>((object, object) key, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == productA.Id;

            var entityBefore = ProductSet.AsNoTracking().Single(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.Delete<ProductTc>((entityBefore.Key1, entityBefore.Key2), shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = ProductSet.AsNoTracking().SingleOrDefault(predicate);
            Assert.Equal(EntityState.Detached, state);

            if (shouldSave)
            {
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: DeleteAsync<TEntity>((object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default) 

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsyncCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            Expression<Func<ProductTc, bool>> predicate = x => x.Id == productA.Id;

            var entityBefore = await ProductSet.AsNoTracking().SingleAsync(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            await deleter.DeleteAsync<ProductTc>((entityBefore.Key1, entityBefore.Key2), shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await ProductSet.AsNoTracking().SingleOrDefaultAsync(predicate);
            Assert.Equal(EntityState.Detached, state);

            if (shouldSave)
            {
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: Delete<TEntity>((object, object, object) key, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrder = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "1", 1);
            AddEnities(productOrder);

            Expression<Func<ProductOrderTc, bool>> predicate = p => p.CompanyId == productOrder.Key1
                && p.LocationId == productOrder.Key2 && p.UserId == productOrder.Key3;

            var entityBefore = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.Delete<ProductOrderTc>((entityBefore.Key1, entityBefore.Key2, entityBefore.Key3), shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate);
            Assert.Equal(EntityState.Detached, state);

            if (shouldSave)
            {
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: DeleteAsync<TEntity>((object, object, object) key, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteAsyncCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrder = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "1", 1);
            AddEnities(productOrder);

            Expression<Func<ProductOrderTc, bool>> predicate = p => p.CompanyId == productOrder.Key1
                && p.LocationId == productOrder.Key2 && p.UserId == productOrder.Key3;

            var entityBefore = await ProductOrderSet.AsNoTracking().SingleAsync(predicate);
            Assert.NotNull(entityBefore);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            await deleter.DeleteAsync<ProductOrderTc>((entityBefore.Key1, entityBefore.Key2, entityBefore.Key3), shouldSave);
            var state = _dbContext.Entry(entityBefore).State;

            var entityAfter = await ProductOrderSet.AsNoTracking().SingleOrDefaultAsync(predicate);
            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.Null(entityAfter);
            }
            else
            {
                Assert.Equal(EntityState.Detached, state);
                Assert.NotNull(entityAfter);
            }
        }

        #endregion

        #region Tests: DeleteRange<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteRange_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var userModerator = DefaultData.UserModerator;
            AddEnities(userAdmin, userManager, userModerator);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            var entityBefore1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var entityBefore2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);
            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.DeleteRange([entityBefore1, entityBefore2], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = UserSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityAfter2 = UserSet.AsNoTracking().SingleOrDefault(predicate2);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.Equal(EntityState.Deleted, state1);
                Assert.Equal(EntityState.Deleted, state2);
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRangeAsync<TEntity>(IEnumerable<TEntity> entities, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteRangeAsync_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var userModerator = DefaultData.UserModerator;
            AddEnities(userAdmin, userManager, userModerator);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            var entityBefore1 = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var entityBefore2 = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate2);
            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            await deleter.DeleteRangeAsync([entityBefore1, entityBefore2], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityAfter2 = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            if (shouldSave)
            {
                Assert.Equal(EntityState.Detached, state1);
                Assert.Equal(EntityState.Detached, state2);
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.Equal(EntityState.Deleted, state1);
                Assert.Equal(EntityState.Deleted, state2);
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRange<TEntity>(IEnumerable<object> keys, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteRangeSingeKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var userModerator = DefaultData.UserModerator;
            AddEnities(userAdmin, userManager, userModerator);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            var entityBefore1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var entityBefore2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);
            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            deleter.DeleteRange<UserTc>([entityBefore1.Key, entityBefore2.Key], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = UserSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityAfter2 = UserSet.AsNoTracking().SingleOrDefault(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRangeAsync<TEntity>(IEnumerable<object> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteRangeAsyncSingeKey_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;
            var userModerator = DefaultData.UserModerator;
            AddEnities(userAdmin, userManager, userModerator);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            var entityBefore1 = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate1);
            var entityBefore2 = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate2);
            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            await deleter.DeleteRangeAsync<UserTc>([entityBefore1.Key, entityBefore2.Key], shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityAfter2 = await UserSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRange<TEntity>(IEnumerable<(object,object)> keys, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteRangeCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == productA.Id;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == productB.Id;

            var entityBefore1 = ProductSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityBefore2 = ProductSet.AsNoTracking().SingleOrDefault(predicate2);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            IEnumerable<(object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2),
                (entityBefore2.Key1, entityBefore2.Key2)];

            deleter.DeleteRange<ProductTc>(keys, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = ProductSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityAfter2 = ProductSet.AsNoTracking().SingleOrDefault(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRangeAsync<TEntity>(IEnumerable<(object,object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteRangeAsyncCompositeKey2_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productA = DefaultData.ProductA;
            var productB = DefaultData.ProductB;
            var productC = DefaultData.ProductC;
            AddEnities(productA, productB, productC);

            Expression<Func<ProductTc, bool>> predicate1 = x => x.Id == productA.Id;
            Expression<Func<ProductTc, bool>> predicate2 = x => x.Id == productB.Id;

            var entityBefore1 = await ProductSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityBefore2 = await ProductSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            IEnumerable<(object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2),
                (entityBefore2.Key1, entityBefore2.Key2)];

            await deleter.DeleteRangeAsync<ProductTc>(keys, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await ProductSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityAfter2 = await ProductSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRange<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false)

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteRangeCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrderA = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "1", 1);
            var productOrderB = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "2", 2);
            var productOrderC = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "3", 3);
            AddEnities(productOrderA, productOrderB, productOrderC);

            Expression<Func<ProductOrderTc, bool>> predicate1 = p => p.CompanyId == productOrderA.Key1
                && p.LocationId == productOrderA.Key2 && p.UserId == productOrderA.Key3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = p => p.CompanyId == productOrderB.Key1
                && p.LocationId == productOrderB.Key2 && p.UserId == productOrderB.Key3;

            var entityBefore1 = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityBefore2 = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate2);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            IEnumerable<(object, object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2, entityBefore1.Key3),
                (entityBefore2.Key1, entityBefore2.Key2, entityBefore2.Key3),
            ];

            deleter.DeleteRange<ProductOrderTc>(keys, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate1);
            var entityAfter2 = ProductOrderSet.AsNoTracking().SingleOrDefault(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion

        #region Tests: DeleteRangeAsync<TEntity>(IEnumerable<(object, object, object)> keys, bool shouldSave = false, CancellationToken cancellationToken = default) 

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DeleteRangeAsyncCompositeKey3_If_Param_ShouldSave_Is_False_Then_Not_Saved(bool shouldSave)
        {
            var productOrderA = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "1", 1);
            var productOrderB = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "2", 2);
            var productOrderC = EntityGenerator.CreateProductOrder(Guid.NewGuid(), "3", 3);
            AddEnities(productOrderA, productOrderB, productOrderC);

            Expression<Func<ProductOrderTc, bool>> predicate1 = p => p.CompanyId == productOrderA.Key1
                && p.LocationId == productOrderA.Key2 && p.UserId == productOrderA.Key3;
            Expression<Func<ProductOrderTc, bool>> predicate2 = p => p.CompanyId == productOrderB.Key1
                && p.LocationId == productOrderB.Key2 && p.UserId == productOrderB.Key3;

            var entityBefore1 = await ProductOrderSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityBefore2 = await ProductOrderSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            Assert.NotNull(entityBefore1);
            Assert.NotNull(entityBefore2);

            var deleter = new EfEntityDeleter<TestDbContext>(_dbContext);

            IEnumerable<(object, object, object)> keys = [
                (entityBefore1.Key1, entityBefore1.Key2, entityBefore1.Key3),
                (entityBefore2.Key1, entityBefore2.Key2, entityBefore2.Key3),
            ];

            await deleter.DeleteRangeAsync<ProductOrderTc>(keys, shouldSave);

            var state1 = _dbContext.Entry(entityBefore1).State;
            var state2 = _dbContext.Entry(entityBefore2).State;

            var entityAfter1 = await ProductOrderSet.AsNoTracking().SingleOrDefaultAsync(predicate1);
            var entityAfter2 = await ProductOrderSet.AsNoTracking().SingleOrDefaultAsync(predicate2);

            Assert.Equal(EntityState.Detached, state1);
            Assert.Equal(EntityState.Detached, state2);

            if (shouldSave)
            {
                Assert.Null(entityAfter1);
                Assert.Null(entityAfter2);
            }
            else
            {
                Assert.NotNull(entityAfter1);
                Assert.NotNull(entityAfter2);
            }
        }

        #endregion
    }
}