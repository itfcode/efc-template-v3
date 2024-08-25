using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ITFCode.Core.DTO.Identity.Models.Base;

namespace ITFCode.Core.DTO.Identity.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApplicationUserTokenDTO : IdentityDTO
    {
        public virtual ApplicationUserDTO User { get; set; }
    }
}