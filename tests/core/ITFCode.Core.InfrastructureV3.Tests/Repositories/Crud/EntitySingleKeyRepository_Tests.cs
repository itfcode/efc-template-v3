using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntitySingleKeyRepository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get(TKey key, bool asNoTracking = true)

        #endregion

        #region Tests: GetAsync(TKey key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: GetMany(IEnumerable<TKey> keys, bool asNoTracking = true)

        #endregion

        #region Tests: GetManyAsync(IEnumerable<TKey> keys, bool asNoTracking = true, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Update(TKey key, Action<TEntity> updater, bool shouldSave = false)

        #endregion

        #region Tests: UpdateAsync(TKey key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: UpdateRange(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false)

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<TKey> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Delete(TKey key, bool shouldSave = false)

        #endregion

        #region Tests: DeleteAsync(TKey key, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: DeleteRange(IEnumerable<TKey> keys, bool shouldSave = false)

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<TKey> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Private Methods 

        private IEntityRepository<UserTc, int> CreateService()
        {
            return new UserTcReporsitory(_dbContext);
        }

        #endregion

    }
}