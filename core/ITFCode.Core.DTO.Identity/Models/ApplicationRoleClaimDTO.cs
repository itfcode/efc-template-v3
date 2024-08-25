using ITFCode.Core.DTO.Identity.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace ITFCode.Core.DTO.Identity.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApplicationRoleClaimDTO : IdentityDTO
    {
        [JsonProperty("roleClaimId")]
        [JsonPropertyName("roleClaimId")]
        public int Id { get; set; }

        public string? RoleId { get; set; }

        public string? ClaimType { get; set; }

        public string? ClaimValue { get; set; }

        public virtual ApplicationRoleDTO? Role { get; set; }
    }
}
