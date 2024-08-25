using ITFCode.Core.DTO.Identity.Models;
using ITFCode.Core.Services.Response;

namespace RlyNetApplication.Core.Service.Identity.Responses
{
    public class IndetityRoleResponse : ServiceResponse<ApplicationRoleDTO>
    {
        #region Constructors 

        public IndetityRoleResponse(ApplicationRoleDTO value) : base(value) { }

        public IndetityRoleResponse(ApplicationRoleDTO value, IEnumerable<string> errors) : base(value, errors) { }

        #endregion
    }
}
