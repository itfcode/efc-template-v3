using AutoMapper;
using ITFCode.Core.Data.Identity.Managers.Interfaces;
using ITFCode.Core.Domain.Identity.Entities;
using ITFCode.Core.DTO.Identity.Models;
using ITFCode.Core.Services.Identity.Base;
using ITFCode.Core.Services.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RlyNetApplication.Core.Service.Identity.Responses;

namespace ITFCode.Core.Services.Identity
{
    public class ApplicationUserService : IdentityBaseService<ApplicationUser, ApplicationUserDTO>, IApplicationUserService
    {
        #region Private & Protected Fields 

        private readonly IApplicationUserManager _applicationUserManager;

        #endregion

        #region Protected Properties 

        protected IApplicationUserManager ApplicationUserManager => _applicationUserManager ?? throw new Exception("Application User Manager not defined");

        #endregion

        #region Constructors 

        public ApplicationUserService(ILogger<ApplicationUserService> logger, IMapper mapper, IApplicationUserManager applicationUserManager) : base(logger, mapper)
        {
            _applicationUserManager = applicationUserManager;
        }

        #endregion

        #region Public Methods : IApplicationUserService Implementation 

        public virtual async Task<IEnumerable<ApplicationUserDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            return await ApplicationUserManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(u => Mapper.Map<ApplicationUserDTO>(u))
                .ToListAsync(cancellationToken);
        }

        public async Task<ApplicationUserDTO> GetById(string userId, CancellationToken cancellationToken = default)
        {
            var user = await ApplicationUserManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception($"User not found. UserId = '{userId}'");

            return Mapper.Map<ApplicationUserDTO>(user);
        }

        public async Task<ApplicationUserDTO> GetByEmail(string userEmail, CancellationToken cancellationToken = default)
        {
            var user = await ApplicationUserManager.FindByNameAsync(userEmail);

            if (user == null)
                throw new Exception($"User not found. User Mail = '{userEmail}'");

            return Mapper.Map<ApplicationUserDTO>(user);
        }

        public async Task<bool> UpdateRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default)
        {
            var user = await ApplicationUserManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception($"User not found. UserId = '{userId}'");

            var existingRoles = user.UserRoles.Select(x => x.Role.Name).ToList();
            var addingRoles = roles.Except(existingRoles).ToList();
            var removingRoles = existingRoles.Except(roles).ToList();

            try
            {
                await ApplicationUserManager.RemoveFromRolesAsync(user, removingRoles);
                await ApplicationUserManager.AddToRolesAsync(user, addingRoles);
            }
            catch (Exception ex)
            {
                // 
            }

            return true;
        }

        public async Task<bool> AddToRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromRoles(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUserDTO> Add(ApplicationUserDTO user, string password, CancellationToken cancellationToken = default)
        {
            //user.UserName = user.Email;

            if (await ApplicationUserManager.FindByEmailAsync(user.Email) != null)
                throw new Exception("User with the same email already exists");

            var res = await ApplicationUserManager.CreateAsync(Map(user), password);

            if (!res.Succeeded)
                throw new Exception($"User not created. " + string.Join(",", res.Errors.Select(x => $"{x.Code}: {x.Description}")));

            return Map(await ApplicationUserManager.FindByEmailAsync(user.Email));
        }

        public async Task<ApplicationUserDTO> Update(ApplicationUserDTO user, CancellationToken cancellationToken = default)
        {
            var entity = await ApplicationUserManager.FindByEmailAsync(user.Email);

            if (entity == null)
                throw new Exception($"User with the email '{user.Email}' doesn`t exist");

            entity.Email = user.Email;
            entity.PhoneNumber = user.PhoneNumber;
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;

            var res = await ApplicationUserManager.UpdateAsync(entity);

            if (!res.Succeeded)
                throw new Exception($"User not updated. " + string.Join(",", res.Errors.Select(x => $"{x.Code}: {x.Description}")));

            return Map(await ApplicationUserManager.FindByEmailAsync(user.Email));
        }

        public async Task<IndetityUserResponse> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new IndetityUserResponse(new List<string> { "UserId is null or empty" });

            ApplicationUser user;

            try
            {
                user = await ApplicationUserManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return new IndetityUserResponse(new List<string> { "User not found" });
                }
            }
            catch (Exception ex)
            {
                return new IndetityUserResponse(new List<string> { ex.Message });
            }

            try
            {
                var res = await ApplicationUserManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!res.Succeeded)
                {
                    return new IndetityUserResponse(res.Errors.Select(x => $"Code:{x.Code}, Description: {x.Description}"));
                }
            }
            catch (Exception ex)
            {
                return new IndetityUserResponse(new List<string> { ex.Message });
            }

            return new IndetityUserResponse(Map(await ApplicationUserManager.FindByIdAsync(userId)));
        }

        #endregion
    }
}