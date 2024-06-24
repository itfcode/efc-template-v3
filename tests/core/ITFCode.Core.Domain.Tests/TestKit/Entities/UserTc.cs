using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class UserTc : Entity<int>
    {
        public override int Key => Id;

        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<UserRoleTc> UserRoles { get; set; } = [];
    }
}
