using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Common.Tests.Entities
{
    public class LocationTc : Entity<string>
    {
        public override string Key => Id;
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}
