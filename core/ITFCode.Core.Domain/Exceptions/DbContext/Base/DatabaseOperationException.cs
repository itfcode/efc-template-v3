namespace ITFCode.Core.Domain.Exceptions.DbContext.Base
{
    public abstract class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string message) : base(message) { }

        public DatabaseOperationException(string message, Exception innerException) : base(message, innerException) { }
    }

}