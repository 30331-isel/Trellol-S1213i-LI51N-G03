namespace RepositoryInterfaces
{
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetSome(params string[] ids);
        T GetById(params string[] id);
        void Add(T t);
        void Edit(T t);
        void Remove(T t);
        bool Exists(params string[] ids);
    }
}