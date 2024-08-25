using ITFCode.Core.Domain.Identity.Entities;

namespace ITFCode.Core.Data.Identity.Managers.Interfaces
{
    public interface IApplicationRoleManager : IDisposable
    {
        IQueryable<ApplicationRole> Roles { get; }
    }
}