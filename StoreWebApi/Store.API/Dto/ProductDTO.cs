using Store.DataAccess.Postgress.Models;
using System.ComponentModel.DataAnnotations;

namespace Store.API.Dto
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        public string Category { get; set; } = string.Empty;
        [Required, Range(1, double.MaxValue)]
        public double Price { get; set; } = 0;
        public long AvailableStock { get; set; } = 0; // число закупленных экземпляров товара
        public Guid Image { get; set; }
    }
}
