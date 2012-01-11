using System;
using System.Collections.Generic;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public class ContactRepository : IRepository<Contact>
    {
        readonly IContactDb _db;
        public ContactRepository(IContactDb db)
        {
            _db = db;
        }

        private static Contact ToContact(IDataRecord dataRecord)
        {
            var item = new Contact
                           {
                               ItemId = Convert.ToInt32(dataRecord["ItemId"]),
                               ModuleId = Convert.ToInt32(dataRecord["ModuleId"])
                           };
            return item;
        }

        public List<Contact> GetList(int moduleId)
        {
            IDataReader dr = _db.GetContacts(moduleId);
            var list = new List<Contact>();

            while (dr.Read())
            {
                list.Add(ToContact(dr));
            }

            dr.Close();
            return list;
        }

        public Contact GetSingle(int itemId)
        {
            IDataReader dr = _db.GetSingleContact(itemId);
            var item = new Contact();

            while (dr.Read())
            {
                item = ToContact(dr);
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
                _db.DeleteContact(itemId);
            }
        }

        public Contact Save(Contact item)
        {
            if (item.ItemId == 0)
            {
                item.ItemId = _db.AddContact(item.ModuleId, item.CreatedByUser, item.Name, item.Role,
                                             item.Email, item.Contact1, item.Contact2);

            }
            else
            {
                _db.UpdateContact(item.ItemId, item.CreatedByUser, item.Name, item.Role,
                                  item.Email, item.Contact1, item.Contact2);
            }
            return GetSingle(item.ItemId);
        }
    }
}