using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.Common.Tests.TestKit;
using ITFCode.Core.Domain.Entities.Base;
using ITFCode.Core.Domain.Entities.Base.Interfaces;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntitySingleKeyRepository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get(TKey key)

        [Fact]
        public override void Get_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            var userId = DefaultData.UserAdmin.Id;
            var user = repository.Get(userId);

            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user).State);
        }

        #endregion

        #region Tests: GetAsync(TKey key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();
            var userId = DefaultData.UserAdmin.Id;

            var user = await repository.GetAsync(userId);

            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user).State);
        }

        [Fact]
        public override async Task GetAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetAsync(DefaultData.UserAdmin.Key, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: GetMany(IEnumerable<TKey> keys)

        [Fact]
        public override void GetMany_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var repository = CreateRepository();
            var userAdminId = DefaultData.UserAdmin.Id;
            var userManagerId = DefaultData.UserManager.Id;
            IEnumerable<int> keys = [userAdminId, userManagerId];

            var users = repository.GetMany(keys);

            Assert.NotEmpty(users);
            Assert.Equal(2, users.Count);
            Assert.True(users.All(x => keys.Contains(x.Key)));
            Assert.True(users.All(u => _dbContext.Entry(u).State == EntityState.Detached));
        }

        #endregion

        #region Tests: GetManyAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task GetManyAsync_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var repository = CreateRepository();
            var userAdminId = DefaultData.UserAdmin.Id;
            var userManagerId = DefaultData.UserManager.Id;
            IEnumerable<int> keys = [userAdminId, userManagerId];

            var users = await repository.GetManyAsync(keys);

            Assert.NotEmpty(users);
            Assert.Equal(2, users.Count);
            Assert.True(users.All(x => keys.Contains(x.Key)));
            Assert.True(users.All(u => _dbContext.Entry(u).State == EntityState.Detached));
        }

        [Fact]
        public override async Task GetManyAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<int> keys = [DefaultData.UserAdmin.Id, DefaultData.UserManager.Id];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.GetManyAsync(keys, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Insert(TEntity entity)

        [Fact]
        public override void Insert_If_Param_Is_Correct_Then_Ok()
        {
            var user = DefaultData.UserAdmin;

            var repository = CreateRepository();

            var entity = repository.Insert(user);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == user.Id;

            Assert.NotNull(user);
            Assert.Equal(user.Id, entity.Id);
            Assert.Equal(EntityState.Added, _dbContext.Entry(user).State);
            Assert.Null(UserSet.FirstOrDefault(predicate));

            repository.Commit();

            Assert.NotNull(UserSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user).State);
        }

        #endregion

        #region Tests: InsertAsync(TEntity entity, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertAsync_If_Param_Is_Correct_Then_Ok()
        {
            var user = DefaultData.UserAdmin;

            var repository = CreateRepository();

            var entity = await repository.InsertAsync(user);

            Expression<Func<UserTc, bool>> predicate = x => x.Id == user.Id;

            Assert.NotNull(user);
            Assert.Equal(user.Id, entity.Id);
            Assert.Equal(EntityState.Added, _dbContext.Entry(user).State);
            Assert.Null(await UserSet.FirstOrDefaultAsync(predicate));

            repository.Commit();

            Assert.NotNull(UserSet.FirstOrDefault(predicate));
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user).State);
        }

        [Fact]
        public override async Task InsertAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            var user = DefaultData.UserAdmin;

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertAsync(user, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: InsertRange(IEnumerable<TEntity> entities)

        [Fact]
        public override void InsertRange_If_Param_Is_Correct_Then_Ok()
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;

            var repository = CreateRepository();

            repository.InsertRange([userAdmin, userManager]);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            Assert.Equal(EntityState.Added, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Added, _dbContext.Entry(userManager).State);

            Assert.Null(UserSet.FirstOrDefault(predicate1));
            Assert.Null(UserSet.FirstOrDefault(predicate2));

            repository.Commit();

            Assert.Equal(EntityState.Detached, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userManager).State);

            Assert.NotNull(UserSet.FirstOrDefault(predicate1));
            Assert.NotNull(UserSet.FirstOrDefault(predicate2));
        }

        #endregion

        #region Tests: InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok()
        {
            var userAdmin = DefaultData.UserAdmin;
            var userManager = DefaultData.UserManager;

            var repository = CreateRepository();

            await repository.InsertRangeAsync([userAdmin, userManager]);

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userManager.Id;

            Assert.Equal(EntityState.Added, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Added, _dbContext.Entry(userManager).State);

            Assert.Null(await UserSet.FirstOrDefaultAsync(predicate1));
            Assert.Null(await UserSet.FirstOrDefaultAsync(predicate2));

            repository.Commit();

            Assert.Equal(EntityState.Detached, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userManager).State);

            Assert.NotNull(await UserSet.FirstOrDefaultAsync(predicate1));
            Assert.NotNull(await UserSet.FirstOrDefaultAsync(predicate2));
        }

        [Fact]
        public override async Task InsertRangeAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            IEnumerable<UserTc> users = [DefaultData.UserAdmin, DefaultData.UserManager];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.InsertRangeAsync(users, cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Update(TKey key, Action<TEntity> updater)

        [Fact]
        public override void Update_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            Expression<Func<UserTc, bool>> predicate = x => x.Id == DefaultData.UserAdmin.Id;

            var userAdmin = UserSet.FirstOrDefault(predicate);
            Assert.NotNull(userAdmin);

            var newFirstName = $"New_{userAdmin.FirstName}";

            var repository = CreateRepository();

            userAdmin.FirstName = newFirstName;
            repository.Update(userAdmin);

            Assert.Equal(EntityState.Modified, _dbContext.Entry(userAdmin).State);

            var userAdminBefore = UserSet.AsNoTracking().FirstOrDefault(predicate);
            Assert.NotNull(userAdminBefore);
            Assert.NotEqual(newFirstName, userAdminBefore.FirstName);

            repository.Commit();

            var userAdminAfter = UserSet.AsNoTracking().FirstOrDefault(predicate);
            Assert.NotNull(userAdminAfter);
            Assert.Equal(newFirstName, userAdminAfter.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userAdmin).State);
        }

        #endregion

        #region Tests: UpdateAsync(TKey key, Action<TEntity> updater, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task UpdateAsync_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            Expression<Func<UserTc, bool>> predicate = x => x.Id == DefaultData.UserAdmin.Id;

            var userAdmin = await UserSet.FirstOrDefaultAsync(predicate);
            Assert.NotNull(userAdmin);

            var newFirstName = $"New_{userAdmin.FirstName}";

            var repository = CreateRepository();

            userAdmin.FirstName = newFirstName;
            repository.Update(userAdmin);

            Assert.Equal(EntityState.Modified, _dbContext.Entry(userAdmin).State);

            var userAdminBefore = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            Assert.NotNull(userAdminBefore);
            Assert.NotEqual(newFirstName, userAdminBefore.FirstName);

            repository.Commit();

            var userAdminAfter = await UserSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            Assert.NotNull(userAdminAfter);
            Assert.Equal(newFirstName, userAdminAfter.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userAdmin).State);
        }

        [Fact]
        public override async Task UpdateAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();
            var user = DefaultData.UserAdmin;

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(user, cancellationToken: cancellationToken));

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateAsync(user.Id, x => x.FirstName = "NewFirstName", cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater)

        [Fact]
        public override void UpdateRange_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == DefaultData.UserManager.Id;

            var user1 = UserSet.FirstOrDefault(predicate1);
            var user2 = UserSet.FirstOrDefault(predicate2);

            Assert.NotNull(user1);
            Assert.NotNull(user2);

            var newFirstName1 = $"New_{user1.FirstName}";
            var newFirstName2 = $"New_{user2.FirstName}";

            var repository = CreateRepository();

            user1.FirstName = newFirstName1;
            user2.FirstName = newFirstName2;

            repository.UpdateRange([user1, user2]);

            Assert.Equal(EntityState.Modified, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(user2).State);

            var userBefore1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userBefore2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userBefore1);
            Assert.NotNull(userBefore2);
            Assert.NotEqual(newFirstName1, userBefore1.FirstName);
            Assert.NotEqual(newFirstName2, userBefore2.FirstName);

            repository.Commit();

            var userAfter1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userAfter2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userAfter1);
            Assert.NotNull(userAfter2);
            Assert.Equal(newFirstName1, userAfter1.FirstName);
            Assert.Equal(newFirstName2, userAfter2.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user2).State);
        }

        [Fact]
        public override void UpdateRange_If_Params_Are_Correct_Then_Ok()
        {
            AddTestingData();

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == DefaultData.UserManager.Id;

            var user1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var user2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(user1);
            Assert.NotNull(user2);

            var repository = CreateRepository();

            repository.UpdateRange([user1.Key, user2.Key], x => x.FirstName = $"New{x.FirstName}");

            Assert.Equal(EntityState.Detached, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user2).State);

            var userBefore1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userBefore2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userBefore1);
            Assert.NotNull(userBefore2);
            Assert.NotEqual($"New{user1.FirstName}", userBefore1.FirstName);
            Assert.NotEqual($"New{user2.FirstName}", userBefore2.FirstName);

            repository.Commit();

            var userAfter1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userAfter2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userAfter1);
            Assert.NotNull(userAfter2);
            Assert.Equal($"New{user1.FirstName}", userAfter1.FirstName);
            Assert.Equal($"New{user2.FirstName}", userAfter2.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user2).State);
        }

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task UpdateRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == DefaultData.UserManager.Id;

            var userAdmin = UserSet.FirstOrDefault(predicate1);
            var userManager = UserSet.FirstOrDefault(predicate2);

            Assert.NotNull(userAdmin);
            Assert.NotNull(userManager);

            var newFirstName1 = $"New_{userAdmin.FirstName}";
            var newFirstName2 = $"New_{userManager.FirstName}";

            var repository = CreateRepository();

            userAdmin.FirstName = newFirstName1;
            userManager.FirstName = newFirstName2;
            repository.UpdateRange([userAdmin, userManager]);

            Assert.Equal(EntityState.Modified, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(userManager).State);

            var userAdminBefore = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userManagerBefore = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userAdminBefore);
            Assert.NotNull(userManagerBefore);
            Assert.NotEqual(newFirstName1, userAdminBefore.FirstName);
            Assert.NotEqual(newFirstName2, userManagerBefore.FirstName);

            repository.Commit();

            var userAdminAfter = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userManagerAfter = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userAdminAfter);
            Assert.NotNull(userManagerAfter);
            Assert.Equal(newFirstName1, userAdminAfter.FirstName);
            Assert.Equal(newFirstName2, userManagerAfter.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userAdmin).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(userManager).State);
        }

        [Fact]
        public override async Task UpdateRangeAsync_If_Params_Are_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == DefaultData.UserManager.Id;

            var user1 = UserSet.FirstOrDefault(predicate1);
            var user2 = UserSet.FirstOrDefault(predicate2);

            Assert.NotNull(user1);
            Assert.NotNull(user2);

            var repository = CreateRepository();

            repository.UpdateRange([user1.Key, user2.Key], x => x.FirstName = $"New_{x.FirstName}");

            Assert.Equal(EntityState.Modified, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Modified, _dbContext.Entry(user2).State);

            var userBefore1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userBefore2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userBefore1);
            Assert.NotNull(userBefore2);
            Assert.NotEqual($"New_{user1.FirstName}", userBefore1.FirstName);
            Assert.NotEqual($"New_{user2.FirstName}", userBefore2.FirstName);

            repository.Commit();

            var userAfter1 = UserSet.AsNoTracking().FirstOrDefault(predicate1);
            var userAfter2 = UserSet.AsNoTracking().FirstOrDefault(predicate2);

            Assert.NotNull(userAfter1);
            Assert.NotNull(userAfter2);
            Assert.NotEqual($"New_{user1.FirstName}", userAfter1.FirstName);
            Assert.NotEqual($"New_{user2.FirstName}", userAfter2.FirstName);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user1).State);
            Assert.Equal(EntityState.Detached, _dbContext.Entry(user2).State);
        }

        [Fact]
        public override async Task UpdateRangeAsync_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<UserTc> users = [DefaultData.UserAdmin, DefaultData.UserManager];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(users, cancellationToken: cancellationToken));

            IEnumerable<int> keys = users.Select(x => x.Id);

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.UpdateRangeAsync(keys, x => x.FirstName = "NewFirstName", cancellationToken: cancellationToken));
        }

        #endregion

        #region Tests: Delete(TKey key)

        [Fact]
        public override void Delete_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var userId = DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate = x => x.Id == userId;

            var repository = CreateRepository();
            repository.Delete(userId);

            var entity = UserSet.FirstOrDefault(predicate);
            Assert.NotNull(entity);

            repository.Commit();

            entity = UserSet.FirstOrDefault(predicate);
            Assert.Null(entity);
        }

        #endregion

        #region Tests: DeleteAsync(TKey key, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteAsync_By_Key_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var userId = DefaultData.UserAdmin.Id;
            Expression<Func<UserTc, bool>> predicate = x => x.Id == userId;

            var repository = CreateRepository();
            await repository.DeleteAsync(userId);

            var userBefore = await UserSet.FirstOrDefaultAsync(predicate);
            Assert.NotNull(userBefore);

            repository.Commit();

            var userAfter = await UserSet.FirstOrDefaultAsync(predicate);
            Assert.Null(userAfter);
        }

        public override Task DeleteAsync_Throw_If_Cancellation()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tests: DeleteRange(IEnumerable<TKey> keys)

        [Fact]
        public override void DeleteRange_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            AddTestingData();

            var userId1 = DefaultData.UserAdmin.Id;
            var userId2 = DefaultData.UserManager.Id;

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userId1;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userId2;

            var repository = CreateRepository();
            repository.DeleteRange([userId1, userId2]);

            var userBefore1 = UserSet.FirstOrDefault(predicate1);
            var userBefore2 = UserSet.FirstOrDefault(predicate1);

            Assert.NotNull(userBefore1);
            Assert.NotNull(userBefore2);

            repository.Commit();

            var userAfter1 = UserSet.FirstOrDefault(predicate1);
            var userAfter2 = UserSet.FirstOrDefault(predicate1);

            Assert.Null(userAfter1);
            Assert.Null(userAfter2);
        }

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok()
        {
            await AddTestingDataAsync();

            var userId1 = DefaultData.UserAdmin.Id;
            var userId2 = DefaultData.UserManager.Id;

            Expression<Func<UserTc, bool>> predicate1 = x => x.Id == userId1;
            Expression<Func<UserTc, bool>> predicate2 = x => x.Id == userId2;

            var repository = CreateRepository();
            await repository.DeleteRangeAsync([userId1, userId2]);

            var userBefore1 = await UserSet.FirstOrDefaultAsync(predicate1);
            var userBefore2 = await UserSet.FirstOrDefaultAsync(predicate1);

            Assert.NotNull(userBefore1);
            Assert.NotNull(userBefore2);

            await repository.CommitAsync();

            var userAfter1 = UserSet.FirstOrDefault(predicate1);
            var userAfter2 = UserSet.FirstOrDefault(predicate1);

            Assert.Null(userAfter1);
            Assert.Null(userAfter2);
        }

        [Fact]
        public override async Task DeleteRangeAsync_By_Keys_Throw_If_Cancellation()
        {
            var repository = CreateRepository();
            var cancellationToken = CreateCancellationToken();

            IEnumerable<UserTc> users = [DefaultData.UserAdmin, DefaultData.UserManager];

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => repository.DeleteRangeAsync(users, cancellationToken: cancellationToken));
        }

        #endregion

        #region Private Methods 

        private IEntityRepository<UserTc, int> CreateRepository()
        {
            return new UserTcReporsitory(_dbContext);
        }

        #endregion
    }
}