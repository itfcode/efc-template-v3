using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
    }

    public abstract class Entity<TKey> : Entity, IEntity<TKey> where TKey: IEquatable<TKey>
    {
        public abstract TKey Key { get; }
    }

    public abstract class Entity<TKey1, TKey2> : Entity, IEntity<TKey1, TKey2> 
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        public abstract TKey1 Key1 { get; }
        public abstract TKey2 Key2 { get; }
        public (TKey1 Key1, TKey2 Key2) Key => (Key1, Key2);
    }

    public abstract class Entity<TKey1, TKey2, TKey3 > : Entity, IEntity<TKey1, TKey2, TKey3>
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
    {
        public abstract TKey1 Key1 { get; }
        public abstract TKey2 Key2 { get; }
        public abstract TKey3 Key3 { get; }
        public (TKey1 Key1, TKey2 Key2, TKey3 Key3) Key => (Key1, Key2, Key3);
    }
}