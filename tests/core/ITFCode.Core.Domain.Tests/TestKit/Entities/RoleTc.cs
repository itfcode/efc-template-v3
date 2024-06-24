using ITFCode.Core.Domain.Entities.Base.Interfaces;

namespace ITFCode.Core.Domain.Tests.TestKit.Entities
{
    public class RoleTc : IEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserRoleTc> UserRoles { get; set; } = [];
    }
}
