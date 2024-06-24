using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class ProductTc : Entity<long>
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public long Code { get; set; }

        public override long Key => Id;
    }
}