using ITFCode.Core.Services.Identity.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ITFCode.Core.Services.Identity
{
    public static class DependencyInjectionConfig
    {
        public static void Register(IServiceCollection services)
        {
            // services registration of Identity
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
        }
    }
}