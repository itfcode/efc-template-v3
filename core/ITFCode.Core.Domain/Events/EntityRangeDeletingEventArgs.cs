namespace ITFCode.Core.Domain.Events
{
    public class EntityRangeDeletingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}