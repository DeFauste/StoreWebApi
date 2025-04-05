using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ProductReadDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;
        [JsonPropertyName("price")]
        public double Price { get; set; } = 0;
        [JsonPropertyName("available_stock ")]
        public long AvailableStock { get; set; } = 0;
        [JsonPropertyName("image_id")]
        public Guid ImageId { get; set; } = Guid.Empty;
    }
}
