
namespace Store.DataAccess.Postgress.Repositories
{
    public interface IRepositiry<T, D, ID>
    {
        void Create(T entity);
        void DeleteById(ID id);
        T FindById(ID id);
        IEnumerable<T> FindAll();
        void Update(T entity, D data);
        bool SaveChange();
        bool CanConnection();

    }
}
