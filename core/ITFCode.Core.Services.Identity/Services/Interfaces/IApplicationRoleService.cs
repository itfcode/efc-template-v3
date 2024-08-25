using ITFCode.Core.DTO.Identity.Models;

namespace ITFCode.Core.Services.Identity.Interfaces
{
    public interface IApplicationRoleService
    {
        Task<IEnumerable<ApplicationRoleDTO>> GetAll(CancellationToken cancellationToken = default);
    }
}