using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ImageCreateDTO
    {
        [JsonPropertyName("image")]
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
