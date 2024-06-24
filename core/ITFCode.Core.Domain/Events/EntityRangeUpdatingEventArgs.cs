namespace ITFCode.Core.Domain.Events
{
    public class EntityRangeUpdatingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}