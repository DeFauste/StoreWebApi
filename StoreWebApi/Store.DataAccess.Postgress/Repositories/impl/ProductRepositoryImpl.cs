using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly StoreDbContext _dbContext;
        public ProductRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ProductEntity Add(ProductEntity entity)
        {
            var creted = _dbContext.Product.Add(entity);
            _dbContext.SaveChanges();
            return creted.Entity;
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Product
                .Where(p => p.Id == id)
                .ExecuteDelete();
        }

        public List<ProductEntity> FindAll()
        {
            return _dbContext.Product
                .AsNoTracking()
                .ToList();
        }

        public ProductEntity? FindById(Guid id)
        {
            return  _dbContext.Product
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public ProductEntity? FindBySupplier(SupplierEntiry supplier)
        {
            return _dbContext.Product
                .AsNoTracking()
                .FirstOrDefault(p => p.Supplier == supplier);
                
        }

        public ProductEntity? FindBySupplier(ImagesEntity image)
        {
            return _dbContext.Product
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == image.Id);
        }

        public void Update(ProductEntity entity, long decrease)
        {
            var product = _dbContext.Product
                .FirstOrDefault(p => p.Id == entity.Id)
                ?? throw new Exception();
            if(decrease != 0)
                product.AvailableStock -= decrease;
            if(entity.ImageId != Guid.Empty)
                product.ImageId = entity.ImageId;
            _dbContext.SaveChanges();    
        }
        public bool CanConnection()
        {
            return _dbContext.Database.CanConnect();
        }
    }
}
