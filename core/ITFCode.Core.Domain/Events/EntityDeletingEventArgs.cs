namespace ITFCode.Core.Domain.Events
{
    public class EntityDeletingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}