namespace ITFCode.Core.Domain.Events
{
    public class EntityInsertingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}