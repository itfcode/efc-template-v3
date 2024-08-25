using ITFCode.Core.DTO.Identity.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace ITFCode.Core.DTO.Identity.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ApplicationUserDTO : IdentityDTO
    {
        [JsonProperty("userId")]
        [JsonPropertyName("userId")]
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string NormalizedUserName { get; set; }
        public required string Email { get; set; }
        public required string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<ApplicationUserClaimDTO> Claims { get; set; } = [];
        public virtual ICollection<ApplicationUserLoginDTO> Logins { get; set; } = [];
        public virtual ICollection<ApplicationUserTokenDTO> Tokens { get; set; } = [];
        public virtual ICollection<ApplicationUserRoleDTO> UserRoles { get; set; } = [];
    }
}