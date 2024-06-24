using ITFCode.Core.Domain.Exceptions.DbContext.Base;

namespace ITFCode.Core.Domain.Exceptions.DbContext
{
    public class DeleteRangeEntityException(string message, Exception innerException) : DatabaseOperationException(message, innerException)
    {
    }
}
