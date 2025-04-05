using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class AddressCreateDTO
    {
        [Required, MaxLength(30)]
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        [JsonPropertyName("street")]
        public string Street { get; set; } = string.Empty;
    }
}
