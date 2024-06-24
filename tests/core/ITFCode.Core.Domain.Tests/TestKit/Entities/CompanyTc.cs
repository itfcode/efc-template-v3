using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class CompanyTc : Entity<Guid>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public override Guid Key => Id;
    }
}
