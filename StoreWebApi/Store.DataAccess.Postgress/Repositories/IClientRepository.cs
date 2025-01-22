using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IClientRepository: IRepositiry<ClientEntity, AddressEntity, Guid>
    {
        List<ClientEntity> FindAll(int limit, int page);
        List<ClientEntity> FindClient(string name, string surname);
    }
}
