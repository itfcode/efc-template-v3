namespace ITFCode.Core.Domain.Events
{
    public class EntityUpdatingEventArgs(object data) : EventArgs
    {
        public object Data { get; } = data;
    }
}