using ITFCode.Core.DTO.Identity.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace ITFCode.Core.DTO.Identity.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApplicationRoleDTO : IdentityDTO
    {
        [JsonProperty("roleId")]
        [JsonPropertyName("roleId")]
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string NormalizedName { get; set; }

        public required string ConcurrencyStamp { get; set; }

        public required string Description { get; set; }

        public virtual ICollection<ApplicationUserRoleDTO> UserRoles { get; set; } = [];

        public virtual ICollection<ApplicationRoleClaimDTO> RoleClaims { get; set; } = [];
    }
}