using System.Linq;

namespace EFCore.Mock.Example.Application.Repositories
{
    public interface ICrudRepository<T>
    {
        IQueryable<T> GetAll();

        T GetById(long id);

        bool IsExist(string url);

        T Create(T item);

        T Update(T item);

        T Delete(long id);
    }
}