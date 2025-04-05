using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class AddressReadDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
    }
}
