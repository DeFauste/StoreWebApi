using Store.DataAccess.Postgress.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class SupplierDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
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
