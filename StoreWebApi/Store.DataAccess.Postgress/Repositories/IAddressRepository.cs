using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IAddressRepository : IRepositiry<AddressEntity, AddressEntity, Guid>
    {
    }
}
