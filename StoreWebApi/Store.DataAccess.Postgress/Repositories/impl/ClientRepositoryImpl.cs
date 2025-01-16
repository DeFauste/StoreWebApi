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

        public async Task DeleteById(Guid id)
        {
            await _dbContext.Client
                    .Where(c => c.Id == id)
                    .ExecuteDeleteAsync();
        }

        public async Task<List<ClientEntity>> FindAll()
        {
            return await _dbContext.Client
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<List<ClientEntity>> FindAll(int limit, int page)
        {
            return await _dbContext.Client
                        .AsNoTracking()
                        .Skip((page - 1) * limit)
                        .Take(limit)
                        .ToListAsync();
        }

        public async Task<ClientEntity?> FindById(Guid id)
        {
            return await _dbContext.Client
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ClientEntity>> FindClient(string name, string surname)
        {
            return await _dbContext.Client
                .AsNoTracking()
                .Where(c => c.ClientName == name && c.ClientSurname == surname)
                .ToListAsync();
        }

        public async Task Add(ClientEntity entity)
        {
            await _dbContext.Client.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(ClientEntity entity, AddressEntity data)
        {
            var client = await _dbContext.Client
                .FirstOrDefaultAsync(c => c.Id == entity.Id)
                ?? throw new Exception();
            client.AddressId = data.Id;
            client.Address = data;

            await _dbContext.Client
                .Where(c => c.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.AddressId, data.Id)
                );
        }
    }
}
