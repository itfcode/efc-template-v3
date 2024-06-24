namespace ITFCode.Core.Domain.Exceptions.Collections
{
    public class CollectionIsEmptyException : Exception
    {
        public CollectionIsEmptyException()
            : base("Collection is empty.")
        {
        }

        public CollectionIsEmptyException(string message)
            : base(message)
        {
        }

        public CollectionIsEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}