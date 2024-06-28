using ITFCode.Core.InfrastructureV3.Tests.Repositories.Readonly;

namespace ITFCode.Core.InfrastructureV3.Tests.Repositories.Crud
{
    public abstract class EntityBaseRepository_Tests : EntityReadonlyBaseRepository_Tests
    {

        #region Tests: Get, GetAsync, GetMany & GetManyAsync
        #endregion

        #region Tests: Insert, InsertAsync, InsertRange & InsertRangeAsync

        public abstract void Insert_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertAsync_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertAsync_Throw_If_Cancellation();

        public abstract void InsertRange_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertRangeAsync_If_Param_Is_Correct_Then_Ok();
        public abstract Task InsertRangeAsync_Throw_If_Cancellation();

        #endregion

        #region Tests: Update, UpdateAsync, UpdateMany & UpdateManyAsync

        public abstract void Update_By_Key_If_Param_Is_Correct_Then_Ok();
        public abstract Task UpdateAsync_By_Key_If_Param_Is_Correct_Then_Ok();
        public abstract void UpdateRange_If_Params_Are_Correct_Then_Ok();
        public abstract Task UpdateAsync_Throw_If_Cancellation();

        public abstract void UpdateRange_By_Keys_If_Param_Is_Correct_Then_Ok();
        public abstract Task UpdateRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok();
        public abstract Task UpdateRangeAsync_If_Params_Are_Correct_Then_Ok();
        public abstract Task UpdateRangeAsync_Throw_If_Cancellation();

        #endregion

        #region Tests: Delete, DeleteAsync, DeleteMany & DeleteManyAsync

        public abstract void Delete_By_Key_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteAsync_By_Key_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteAsync_Throw_If_Cancellation();

        public abstract void DeleteRange_By_Keys_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteRangeAsync_By_Keys_If_Param_Is_Correct_Then_Ok();
        public abstract Task DeleteRangeAsync_By_Keys_Throw_If_Cancellation();

        #endregion
    }
}