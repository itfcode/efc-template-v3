using ITFCode.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITFCode.Core.Data.Identity.Managers.Interfaces
{
    public interface IApplicationUserManager : IDisposable
    {
        IQueryable<ApplicationUser> Users { get; }
        Task<ApplicationUser?> FindByIdAsync(string userId);
        Task<ApplicationUser?> FindByNameAsync(string userName);
        Task<ApplicationUser?> FindByEmailAsync(string userMail);
        Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
    }
}