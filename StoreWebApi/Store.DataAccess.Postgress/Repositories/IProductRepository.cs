using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IProductRepository: IRepositiry<ProductEntity, long,Guid>
    {
        Task<ProductEntity?> FindBySupplierAsync(SupplierEntiry supplier);
        Task<ProductEntity?> FindBySupplierAsync(ImagesEntity image);
    }
}
