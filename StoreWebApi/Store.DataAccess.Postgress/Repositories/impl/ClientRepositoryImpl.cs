using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class ClientRepositoryImpl : IClientRepository
    {
        private readonly StoreDbContext _dbContext;
        public ClientRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Client
                    .Where(c => c.Id == id)
                    .ExecuteDelete();
        }

        public  List<ClientEntity> FindAll()
        {
            return  _dbContext.Client
                        .AsNoTracking()
                        .ToList();
        }

        public List<ClientEntity> FindAll(int limit, int page)
        {
            return _dbContext.Client
                        .AsNoTracking()
                        .Skip((page - 1) * limit)
                        .Take(limit)
                        .ToList();
        }

        public ClientEntity? FindById(Guid id)
        {
            return _dbContext.Client
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
        }

        public List<ClientEntity> FindClient(string name, string surname)
        {
            return _dbContext.Client
                .AsNoTracking()
                .Where(c => c.ClientName == name && c.ClientSurname == surname)
                .ToList();
        }

        public ClientEntity Add(ClientEntity entity)
        {
            var created = _dbContext.Client.Add(entity);
            _dbContext.SaveChanges();
 
            return created.Entity;
        }

        public void Update(ClientEntity entity, AddressEntity data)
        {
            _dbContext.Client
                .Where(c => c.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.AddressId, data.Id)
                );
        }

        public bool CanConnection()
        {
            return _dbContext.Database.CanConnect();
        }
    }
}
