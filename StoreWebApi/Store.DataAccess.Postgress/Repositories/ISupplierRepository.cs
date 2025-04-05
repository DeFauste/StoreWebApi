using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface ISupplierRepository: IRepositiry<SupplierEntiry, AddressEntity, Guid>
    {
    }
}
