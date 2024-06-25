using ITFCode.Core.Common.Tests.Entities;
using ITFCode.Core.InfrastructureV3.Repositories.Crud.Interfaces;
using ITFCode.Core.InfrastructureV3.Tests.TestKit.Repositories;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public class EntityCompositeKey2Repository_Tests : EntityBaseRepository_Tests
    {
        #region Tests: Get((TKey1, TKey2) key)

        #endregion

        #region Tests: GetAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: GetMany(IEnumerable<(TKey1, TKey2)> keys)

        #endregion

        #region Tests: GetManyAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: Update((TKey1, TKey2) key, Action<TEntity> updater)

        #endregion

        #region Tests: UpdateAsync((TKey1, TKey2) key, Action<TEntity> updater, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: UpdateRange(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater)

        #endregion

        #region Tests: UpdateRangeAsync(IEnumerable<(TKey1, TKey2)> keys, Action<TEntity> updater, CancellationToken cancellationToken = default) 

        #endregion

        #region Tests: Delete((TKey1, TKey2) key)

        #endregion

        #region Tests: DeleteAsync((TKey1, TKey2) key, CancellationToken cancellationToken = default)

        #endregion

        #region Tests: DeleteRange(IEnumerable<(TKey1, TKey2)> keys) 

        #endregion

        #region Tests: DeleteRangeAsync(IEnumerable<(TKey1, TKey2)> keys, CancellationToken cancellationToken = default)

        #endregion

        #region Private Methods 

        private IEntityRepository<ProductTc, long, string> CreateRepository()
        {
            return new ProductTcReporsitory(_dbContext);
        }

        #endregion
    }
}
