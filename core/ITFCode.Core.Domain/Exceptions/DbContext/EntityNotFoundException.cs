using ITFCode.Core.Domain.Exceptions.DbContext.Base;

namespace ITFCode.Core.Domain.Exceptions.DbContext
{
    public class EntityNotFoundException(object[] key, Type type) :
        DatabaseOperationException($"Item with key {{{string.Join(',', key)}}} not found {type.Name}")
    {
    }
}
