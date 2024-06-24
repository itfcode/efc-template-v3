using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Entities
{
    public class AppSetting : TrackableEntity
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}