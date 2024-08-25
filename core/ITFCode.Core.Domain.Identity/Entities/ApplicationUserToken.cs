using Microsoft.AspNetCore.Identity;

namespace ITFCode.Core.Domain.Identity.Entities
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
