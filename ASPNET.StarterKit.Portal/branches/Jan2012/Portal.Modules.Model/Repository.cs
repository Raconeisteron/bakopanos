using System.Collections.Generic;
using System.Data;
using Portal.Modules.Data;

namespace Portal.Modules.Model
{
    public abstract class Repository<T> : IRepository<T>
        where T : new()
    {
        private readonly IDb _db;

        protected Repository(IDb db)
        {
            _db = db;
        }

        #region IRepository<T> Members

        public List<T> GetList(int moduleId)
        {
            IDataReader dr = _db.GetList(moduleId);

            var list = new List<T>();

            while (dr.Read())
            {
                list.Add(ToItem(dr));
            }

            dr.Close();
            return list;
        }

        public T GetSingle(int itemId)
        {
            IDataReader dr = _db.GetSingle(itemId);

            var item = new T();

            while (dr.Read())
            {
                item = ToItem(dr);
                break;
            }

            dr.Close();

            return item;
        }

        public void Delete(int itemId)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)
            if (itemId != 0)
            {
                _db.Delete(itemId);
            }
        }

        public abstract T Save(T item);

        #endregion

        protected abstract T ToItem(IDataRecord dataRecord);
    }
}