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
        public void Add(SupplierEntiry entity)
        {
           _dbContext.Supplier.Add(entity);
           _dbContext.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Supplier
                .Where(s => s.Id == id)
                .ExecuteDelete();
        }

        public List<SupplierEntiry> FindAll()
        {
            return _dbContext.Supplier
                .AsNoTracking()
                .ToList();
        }

        public SupplierEntiry? FindById(Guid id)
        {
            return _dbContext.Supplier
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(SupplierEntiry entity, AddressEntity data)
        {
           _dbContext.Supplier
                .Where(s => s.Id == entity.Id)
                .ExecuteUpdate(a => a
                .SetProperty(s =>  s.Address, data));
        }
    }
}
