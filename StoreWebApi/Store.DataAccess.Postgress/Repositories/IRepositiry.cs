
namespace Store.DataAccess.Postgress.Repositories
{
    public interface IRepositiry<T, D, ID>
    {
        Task Add(T entity);
        Task DeleteById(ID id);
        Task<T?> FindById(ID id);
        Task<List<T>> FindAll();
        Task Update(T entity, D data);

    }
}
