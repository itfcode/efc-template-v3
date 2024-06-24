using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Common.Tests.Entities
{
    public class ProductTc : Entity<long, string>
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string CountryCode { get; set; }

        public override long Key1 => Id;
        public override string Key2 => CountryCode;
    }
}