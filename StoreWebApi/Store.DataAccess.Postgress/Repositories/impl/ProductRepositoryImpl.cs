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
        public async Task Add(ProductEntity entity)
        {
            await _dbContext.Product.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            await _dbContext.Product
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<ProductEntity>> FindAll()
        {
            return await _dbContext.Product
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductEntity?> FindById(Guid id)
        {
            return await _dbContext.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductEntity?> FindBySupplierAsync(SupplierEntiry supplier)
        {
            return await _dbContext.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Supplier == supplier);
                
        }

        public async Task<ProductEntity?> FindBySupplierAsync(ImagesEntity image)
        {
            return await _dbContext.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Image == image);
        }

        public async Task Update(ProductEntity entity, long decrease)
        {
            var product = await _dbContext.Product
                .FirstOrDefaultAsync(p => p.Id == entity.Id)
                ?? throw new Exception();
            product.AvailableStock -= decrease;
            await _dbContext.SaveChangesAsync();    
        }
    }
}
