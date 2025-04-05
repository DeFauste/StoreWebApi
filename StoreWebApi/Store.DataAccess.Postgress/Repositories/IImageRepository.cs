using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Repositories
{
    public interface IImageRepository : IRepositiry<ImagesEntity, byte[], Guid>
    {
    }
}
