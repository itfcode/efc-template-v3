using ITFCode.Core.DTO.Identity.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace ITFCode.Core.DTO.Identity.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApplicationUserClaimDTO : IdentityDTO
    {
        public virtual ApplicationUserDTO User { get; set; }
    }
}