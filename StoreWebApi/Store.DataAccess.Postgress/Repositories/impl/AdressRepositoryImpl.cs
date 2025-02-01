using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class AdressRepositoryImpl : IAddressRepository
    {
        private readonly StoreDbContext _dbContext;
        public AdressRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AddressEntity Add(AddressEntity entity)
        {
            var created = _dbContext.Address.Add(entity);
            _dbContext.SaveChanges();
            return created.Entity;
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Address
                .Where(a => a.Id == id)
                .ExecuteDelete();
        }

        public List<AddressEntity> FindAll()
        {
            return _dbContext.Address
                .AsNoTracking()
                .ToList();  
        }

        public AddressEntity? FindById(Guid id)
        {
            return _dbContext.Address
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id);
        }

        public void Update(AddressEntity entity, AddressEntity data)
        {
            _dbContext.Address
                .Where(a => a.Id == entity.Id)
                .ExecuteUpdate(s => s
                .SetProperty(a => a.Street, data.Street)
                .SetProperty(a => a.City, data.City)
                .SetProperty(a => a.Country, data.Country));
        }
    }
}
