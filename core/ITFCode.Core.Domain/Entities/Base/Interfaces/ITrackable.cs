namespace ITFCode.Core.Domain.Entities.Base.Interfaces
{
    public interface ITrackable
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
