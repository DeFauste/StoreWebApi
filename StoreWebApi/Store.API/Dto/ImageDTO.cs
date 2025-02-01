
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ImageDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("image")]
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
