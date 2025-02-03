
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store.DataAccess.Postgress.Models
{
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty ;
        public double Price { get; set; } = 0;
        public long AvailableStock { get; set; } = 0; // число закупленных экземпляров товара
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow; // число последней закупки
        public Guid SupplierId { get; set; }
        public SupplierEntiry? Supplier { get; set; }
        public Guid ImageId{ get; set; } = Guid.Empty;
    }
}
