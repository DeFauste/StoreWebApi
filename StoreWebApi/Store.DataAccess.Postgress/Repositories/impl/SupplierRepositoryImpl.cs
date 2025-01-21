using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class SupplierRepositoryImpl : ISupplierRepository
    {
        private readonly StoreDbContext _dbContext;
        public SupplierRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(SupplierEntiry entity)
        {
            await _dbContext.Supplier.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            await _dbContext.Supplier
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<SupplierEntiry>> FindAll()
        {
            return await _dbContext.Supplier
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SupplierEntiry?> FindById(Guid id)
        {
            return await _dbContext.Supplier
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Update(SupplierEntiry entity, AddressEntity data)
        {
           await _dbContext.Supplier
                .Where(s => s.Id == entity.Id)
                .ExecuteUpdateAsync(a => a
                .SetProperty(s =>  s.Address, data));
        }
    }
}
