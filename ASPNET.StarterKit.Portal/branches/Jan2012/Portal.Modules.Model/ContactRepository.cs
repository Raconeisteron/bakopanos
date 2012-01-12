using System;
using System.Data;
using Portal.Modules.Data;

namespace Portal.Modules.Model
{
    public class ContactRepository : Repository<Contact>
    {
        private readonly IContactDb _db;

        public ContactRepository(IContactDb db)
            : base(db)
        {
            _db = db;
        }

        protected override Contact ToItem(IDataRecord dataRecord)
        {
            var item = new Contact
                           {
                               ItemId = Convert.ToInt32(dataRecord["ItemId"]),
                               ModuleId = Convert.ToInt32(dataRecord["ModuleId"]),
                               Contact1 = dataRecord["Contact1"] as string,
                               Contact2 = dataRecord["Contact2"] as string,
                               CreatedByUser = dataRecord["CreatedByUser"] as string,
                               Email = dataRecord["Email"] as string,
                               Name = dataRecord["Name"] as string,
                               Role = dataRecord["Role"] as string,
                               CreatedDate = Convert.ToDateTime(dataRecord["CreatedDate"])
                           };
            return item;
        }

        public override Contact Save(Contact item)
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