
namespace Store.DataAccess.Postgress.Repositories
{
    public interface IRepositiry<T, D, ID>
    {
        void Add(T entity);
        void DeleteById(ID id);
        T? FindById(ID id);
        List<T> FindAll();
        void Update(T entity, D data);

    }
}
