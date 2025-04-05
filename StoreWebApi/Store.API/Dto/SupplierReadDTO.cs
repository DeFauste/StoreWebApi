using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class SupplierReadDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("address_id")]
        public Guid AddressId { get; set; }
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
