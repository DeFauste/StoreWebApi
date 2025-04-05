using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class SupplierCreateDTO
    {
        [Required, MaxLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("address_id")]
        public Guid AddressId { get; set; }
        [Required]
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
