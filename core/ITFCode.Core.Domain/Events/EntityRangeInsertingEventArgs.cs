namespace ITFCode.Core.Domain.Events
{
    public class EntityRangeInsertingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}