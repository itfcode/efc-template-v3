using ITFCode.Core.DTO.Identity.Models;
using RlyNetApplication.Core.Service.Identity.Responses;

namespace ITFCode.Core.Services.Identity.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetAll(CancellationToken cancellationToken = default);
        Task<ApplicationUserDTO> GetById(string userId, CancellationToken cancellationToken = default);
        Task<ApplicationUserDTO> GetByEmail(string userEmail, CancellationToken cancellationToken = default);
        Task<bool> AddToRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default);
        Task<bool> RemoveFromRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default);
        Task<bool> UpdateRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default);
        Task<ApplicationUserDTO> Add(ApplicationUserDTO user, string password, CancellationToken cancellationToken = default);
        Task<ApplicationUserDTO> Update(ApplicationUserDTO user, CancellationToken cancellationToken = default);
        Task<IndetityUserResponse> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}