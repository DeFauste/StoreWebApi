using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class ImageRepository : IImageRepository
    {
        private readonly StoreDbContext _dbContext;
        public ImageRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(ImagesEntity entity)
        {
            await _dbContext.Images.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            await _dbContext.Images
                .Where(i => i.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<ImagesEntity>> FindAll()
        {
            return await _dbContext.Images
                .AsNoTracking()
                .ToListAsync(); 
        }

        public async Task<ImagesEntity?> FindById(Guid id)
        {
            return await _dbContext.Images
                .AsNoTracking()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ImagesEntity entity, byte[] data)
        {
            await _dbContext.Images
                .Where(i => i.Id == entity.Id)
                .ExecuteUpdateAsync(d => d
                .SetProperty(i => i.Image, data));
        }
    }
}
