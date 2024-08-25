using ITFCode.Core.Data.Identity.Managers.Interfaces;
using ITFCode.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ITFCode.Core.Data.Identity.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>, IApplicationRoleManager
    {
        public ApplicationRoleManager(
            IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}