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
        public void Create(SupplierEntiry entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Supplier.Add(entity);
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Supplier
                .Where(s => s.Id == id)
                .ExecuteDelete();
        }

        public IEnumerable<SupplierEntiry> FindAll()
        {
            return _dbContext.Supplier
                .AsNoTracking()
                .ToList();
        }

        public SupplierEntiry FindById(Guid id)
        {
            return _dbContext.Supplier
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(SupplierEntiry entity, AddressEntity data)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _dbContext.Supplier
                .Where(s => s.Id == entity.Id)
                .ExecuteUpdate(a => a
                .SetProperty(s =>  s.AddressId, data.Id));
        }
        public bool CanConnection()
        {
            return _dbContext.Database.CanConnect();
        }

        public bool SaveChange()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}
