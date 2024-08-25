using Microsoft.AspNetCore.Identity;

namespace ITFCode.Core.Domain.Identity.Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
