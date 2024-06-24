using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class ProductOrderTc : Entity<Guid, string, int>
    {
        public override Guid Key1 => CompanyId;
        public override string Key2 => LocationId;
        public override int Key3 => UserId;

        public required Guid CompanyId { get; set; }
        public required string LocationId { get; set; }
        public required int UserId { get; set; }
        public long Code { get; set; }
    }
}