using ITFCode.Core.Domain.Entities.Base;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class UserRoleTc : Entity<int, int>
    {
        public override int Key1 => UserId;
        public override int Key2 => RoleId;
        public required int UserId { get; set; }
        public required int RoleId { get; set; }
        public UserTc? User { get; set; }
        public RoleTc? Role { get; set; }
    }
}