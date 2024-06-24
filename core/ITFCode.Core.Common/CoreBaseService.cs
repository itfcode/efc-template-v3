namespace ITFCode.Core.Common
{
    public abstract class CoreBaseService
    {
        #region Constructors

        protected CoreBaseService()
        {
        }

        #endregion

        #region Protected Methods

        protected virtual T TryGet<T>(T value, string paramName) => value ?? throw new ArgumentNullException(nameof(paramName));

        #endregion
    }
}
