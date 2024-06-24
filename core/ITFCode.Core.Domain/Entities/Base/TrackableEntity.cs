using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Domain.Entities.Base
{
    public abstract class TrackableEntity : Entity, ITrackable
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set ; }
    }

    public abstract class TrackableEntity<TKey> : Entity<TKey>, ITrackable where TKey : IEquatable<TKey>
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}