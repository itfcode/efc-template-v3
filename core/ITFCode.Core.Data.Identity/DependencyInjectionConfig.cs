using ITFCode.Core.Data.Identity.Managers;
using ITFCode.Core.Data.Identity.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITFCode.Core.Data.Identity
{
    public static class DependencyInjectionConfig
    {
        public static void Register(IServiceCollection services)
        {
            // services registration of Identity Entities
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
        }
    }
}
