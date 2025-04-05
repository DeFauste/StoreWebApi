using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ClientCreateDTO
    {
        [Required, MaxLength(30)]
        [JsonPropertyName("client_name")]
        public string ClientName { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        [JsonPropertyName("client_surname")]
        public string ClientSurname { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }
        [JsonPropertyName("gender")]
        public bool Gender { get; set; } = true;
        [Required]
        [JsonPropertyName("address_id")]
        public Guid AddressId { get; set; }
    }
}
