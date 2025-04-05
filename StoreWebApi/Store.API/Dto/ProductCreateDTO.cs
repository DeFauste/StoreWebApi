using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.API.Dto
{
    public class ProductCreateDTO
    {
        [Required, MaxLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;
        [Required, Range(1.0, double.MaxValue)]
        [JsonPropertyName("price")]
        public double Price { get; set; } = 1;
        [JsonPropertyName("available_stock ")]
        public long AvailableStock { get; set; } = 0; // число закупленных экземпляров товара
        [JsonPropertyName("supplier_id")]
        public Guid SupplierId { get; set; }
        [JsonPropertyName("image_id")]
        public Guid ImageId { get; set; } = Guid.Empty;
    }
}
