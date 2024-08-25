using ITFCode.Core.DTO.Identity.Models;
using ITFCode.Core.Services.Response;

namespace RlyNetApplication.Core.Service.Identity.Responses
{
    public class IndetityUserResponse : ServiceResponse<ApplicationUserDTO>
    {
        #region Constructors 

        public IndetityUserResponse(ApplicationUserDTO value) : base(value) { }

        public IndetityUserResponse(IEnumerable<string> errors) : base(errors) { }

        public IndetityUserResponse(ApplicationUserDTO value, IEnumerable<string> errors) : base(value, errors) { }

        #endregion
    }
}