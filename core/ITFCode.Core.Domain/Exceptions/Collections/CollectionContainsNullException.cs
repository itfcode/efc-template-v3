namespace ITFCode.Core.Domain.Exceptions.Collections
{
    public class CollectionContainsNullException : Exception
    {
        public CollectionContainsNullException()
            : base("Collection contains null elements.")
        {
        }

        public CollectionContainsNullException(string message)
            : base(message)
        {
        }

        public CollectionContainsNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
