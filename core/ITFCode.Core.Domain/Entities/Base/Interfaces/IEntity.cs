namespace ITFCode.Core.Domain.Entities.Base.Interfaces
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        TKey Key { get; }
    }

    public interface IEntity<TKey1, TKey2> : IEntity
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        TKey1 Key1 { get; }
        TKey2 Key2 { get; }
        (TKey1 Key1, TKey2 Key2) Key { get; }
    }

    public interface IEntity<TKey1, TKey2, TKey3> : IEntity
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
    {
        TKey1 Key1 { get; }
        TKey2 Key2 { get; }
        TKey3 Key3 { get; }
        (TKey1 Key1, TKey2 Key2, TKey3 Key3) Key { get; }
    }
}