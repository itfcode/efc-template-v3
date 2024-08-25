using Microsoft.AspNetCore.Identity;

namespace ITFCode.Core.Domain.Identity.Entities
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole? Role { get; set; }
    }
}
