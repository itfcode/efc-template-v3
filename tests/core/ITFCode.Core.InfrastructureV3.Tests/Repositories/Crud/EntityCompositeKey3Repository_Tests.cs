using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey3Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2, TKey3) key, bool asNoTracking = true)

        #endregion

        #region Tests: GetAsync((TKey1, TKey2, TKey3) key, bool asNoTracking = true, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true)

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool asNoTracking = true, CancellationToken cancellationToken = default) 

        #endregion

        #region Tests: Update((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false)

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2, TKey3) key, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: UpdateRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false)

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, Action<TEntity> updater, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Delete((TKey1, TKey2, TKey3) key, bool shouldSave = false)

        #endregion

        #region Tests: DeleteAsync((TKey1, TKey2, TKey3) key, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: DeleteRange(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false)

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<(TKey1, TKey2, TKey3)> keys, bool shouldSave = false, CancellationToken cancellationToken = default)

        #endregion

        #region Private Methods 

        private IEntityRepository<ProductOrderTc, Guid, string, int> CreateService()
        {
            return new ProductOrderTcReporsitory(_dbContext);
        }

        #endregion
    }
}
