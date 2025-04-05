using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IProductRepository : IRepositiry<ProductEntity, long, Guid>
    {
        ProductEntity FindBySupplier(SupplierEntiry supplier);
        ProductEntity FindBySupplier(ImagesEntity image);
    }
}
