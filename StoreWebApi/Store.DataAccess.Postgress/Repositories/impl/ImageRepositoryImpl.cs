using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories.impl
{
    public class ImageRepositoryImpl : IImageRepository
    {
        private readonly StoreDbContext _dbContext;
        public ImageRepositoryImpl(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(ImagesEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Images.Add(entity);
        }

        public void DeleteById(Guid id)
        {
            _dbContext.Images
                .Where(i => i.Id == id)
                .ExecuteDelete();
        }

        public IEnumerable<ImagesEntity> FindAll()
        {
            return _dbContext.Images
                .AsNoTracking()
                .ToList();
        }

        public ImagesEntity FindById(Guid id)
        {
            return _dbContext.Images
                .AsNoTracking()
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }

        public void Update(ImagesEntity entity, byte[] data)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _dbContext.Images
                .Where(i => i.Id == entity.Id)
                .ExecuteUpdate(d => d
                .SetProperty(i => i.Image, data));
        }
        public bool SaveChange()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public bool CanConnection()
        {
            return _dbContext.Database.CanConnect();
        }
    }
}
