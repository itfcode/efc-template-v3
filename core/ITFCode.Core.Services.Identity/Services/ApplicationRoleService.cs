using AutoMapper;
using ITFCode.Core.Data.Identity.Managers.Interfaces;
using ITFCode.Core.Domain.Identity.Entities;
using ITFCode.Core.DTO.Identity.Models;
using ITFCode.Core.Services.Identity.Base;
using ITFCode.Core.Services.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ITFCode.Core.Services.Identity
{
    public class ApplicationRoleService : IdentityBaseService<ApplicationRole, ApplicationRoleDTO>, IApplicationRoleService
    {
        #region Private & Protected Fields

        private readonly IApplicationRoleManager _applicationRoleManager;

        #endregion

        #region Protected Properties 

        protected IApplicationRoleManager ApplicationRoleManager => _applicationRoleManager ?? throw new NullReferenceException("ApplicationRoleService: Role Mananger not defined");

        #endregion

        #region Constructors 

        public ApplicationRoleService(ILogger<ApplicationRoleService> logger, IMapper mapper, IApplicationRoleManager applicationRoleManager)
            : base(logger, mapper)
        {
            _applicationRoleManager = applicationRoleManager;
        }

        #endregion

        #region Public Methods : IApplicationRoleService Implenemtation 

        public async Task<IEnumerable<ApplicationRoleDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            return await ApplicationRoleManager.Roles
                .OrderBy(r => r.Name)
                .Select(r => Mapper.Map<ApplicationRoleDTO>(r))
                .ToListAsync(cancellationToken);
        }

        #endregion
    }
}