using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IClientRepository : IRepositiry<ClientEntity, AddressEntity, Guid>
    {
        IEnumerable<ClientEntity> FindAll(int limit, int page);
        IEnumerable<ClientEntity> FindClient(string name, string surname);
    }
}
