using System.Data;

namespace Portal.Modules.Data
{
    public interface IDb
    {
        IDataReader GetList(int moduleId);
        IDataReader GetSingle(int itemId);
        void Delete(int itemId);
    }
}