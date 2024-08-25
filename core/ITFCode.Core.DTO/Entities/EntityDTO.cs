namespace ITFCode.Core.DTO.Entities
{
    public abstract class EntityDTO
    {
    }

    public abstract class EntityDTO<TKey> : EntityDTO where TKey : IEquatable<TKey>
    {
        public required TKey Id { get; set; }
    }

    public abstract class EntityDTO<TKey1, TKey2> : EntityDTO
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
    {
        public abstract TKey1 Key1 { get; }
        public abstract TKey2 Key2 { get; }
    }

    public abstract class EntityDTO<TKey1, TKey2, TKey3> : EntityDTO
        where TKey1 : IEquatable<TKey1>
        where TKey2 : IEquatable<TKey2>
        where TKey3 : IEquatable<TKey3>
    {
        public abstract TKey1 Key1 { get; }
        public abstract TKey2 Key2 { get; }
        public abstract TKey3 Key3 { get; }
    }
}