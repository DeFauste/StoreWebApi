using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class AdressRepositoryImpl : IAddressRepository
    {
        private readonly StoreDbContext _dbContext;
        public AdressRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(AddressEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Address.Add(entity);
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Address
                .Where(a => a.Id == id)
                .ExecuteDelete();
        }

        public IEnumerable<AddressEntity> FindAll()
        {
            return _dbContext.Address
                .AsNoTracking()
                .ToList();
        }

        public AddressEntity FindById(Guid id)
        {
            return _dbContext.Address
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id);
        }

        public void Update(AddressEntity entity, AddressEntity data)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _dbContext.Address
                .Where(a => a.Id == entity.Id)
                .ExecuteUpdate(s => s
                .SetProperty(a => a.Street, data.Street)
                .SetProperty(a => a.City, data.City)
                .SetProperty(a => a.Country, data.Country));
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
