
namespace Store.DataAccess.Postgress.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty ;
        public double Price { get; set; } = 0;
        public int AvailableStock { get; set; } = 0; // число закупленных экземпляров товара
        public DateTime LastUpdateDate { get; set; } = DateTime.Now; // число последней закупки
        public Guid SupplierId { get; set; }
        public SupplierEntiry? Supplier { get; set; }
        public List<ImagesEntity>? Images { get; set; } = [];
    }
}
