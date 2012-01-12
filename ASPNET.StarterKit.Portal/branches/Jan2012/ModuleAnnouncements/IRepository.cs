using System.Collections.Generic;

namespace Portal.Modules.Model
{
    public interface IRepository<T>
    {
        List<T> GetList(int moduleId);
        T GetSingle(int itemId);

        void Delete(int itemId);

        T Save(T item);
    }
}