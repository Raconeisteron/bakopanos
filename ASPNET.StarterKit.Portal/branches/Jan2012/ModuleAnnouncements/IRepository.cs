using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IRepository<T>
    {
        List<T> GetList(int moduleId);
        T GetSingle(int itemId);

        void Delete(int itemId);

        T Save(T item);
    }
}