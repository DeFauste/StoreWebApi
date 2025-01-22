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
        public void Add(ImagesEntity entity)
        {
             _dbContext.Images.Add(entity);
             _dbContext.SaveChanges();
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Images
                .Where(i => i.Id == id)
                .ExecuteDelete();
        }

        public List<ImagesEntity> FindAll()
        {
            return _dbContext.Images
                .AsNoTracking()
                .ToList(); 
        }

        public ImagesEntity? FindById(Guid id)
        {
            return _dbContext.Images
                .AsNoTracking()
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }

        public void Update(ImagesEntity entity, byte[] data)
        {
            _dbContext.Images
                .Where(i => i.Id == entity.Id)
                .ExecuteUpdate(d => d
                .SetProperty(i => i.Image, data));
        }
    }
}
