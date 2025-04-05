using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ClientReadDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("client_name")]
        public string ClientName { get; set; }
        [JsonPropertyName("client_surname")]
        public string ClientSurname { get; set; }
        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }
        [JsonPropertyName("gender")]
        public bool Gender { get; set; }
        [JsonPropertyName("address_id")]
        public Guid AddressId { get; set; }
    }
}
