using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Postgress.Repositories
{
    public class ClientsRepository
    {
        private readonly StoreDbContext _dbContext;
        public ClientsRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ClientEntity>> Get()
        {
            return await _dbContext.Client
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<ClientEntity>> Get(int offset, int limit)
        {
            return await _dbContext.Client
                .AsNoTracking()
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
        public async Task<ClientEntity?> Get(string clientName, string clientSurname)
        {
            return await _dbContext.Client
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClientName == clientName && c.ClientSurname == clientSurname);
        }
        public async Task Add(ClientEntity clientEntity)
        {
            await _dbContext.Client.AddAsync(clientEntity);
            await _dbContext.SaveChangesAsync();    
        }

        public async Task Delete(Guid id)
        {
            await _dbContext.Client
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task UptateAddress(Guid id, AddressEntity addressEntity)
        {
            var client = await _dbContext.Client.FirstOrDefaultAsync(c => c.Id == id) 
                ?? throw new Exception();  
            client.AddressId = id;
            client.Address = addressEntity;
            await _dbContext.Client
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.AddressId, addressEntity.Id)
                );
        }
    }
}
