using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IClientRepository: IRepositiry<ClientEntity, AddressEntity, Guid>
    {
        Task<List<ClientEntity>> FindAll(int limit, int page);
        Task<List<ClientEntity>> FindClient(string name, string surname);
    }
}
