using Microsoft.AspNetCore.Identity;

namespace ITFCode.Core.Domain.Identity.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = new List<ApplicationRoleClaim>();
    }
}
