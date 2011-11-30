using Portal.Modules.Data;
using Portal.Modules.Service.Contracts;

namespace Portal.Modules.Service
{
    internal class ContactService : IContactService
    {
        #region IContactService Members

        public void CreateOrUpdate(PortalContact item)
        {
            IContactsDb contactsDb = ModulesDataAccess.ContactsDb;

            if (item.ItemId == 0)
            {
                contactsDb.AddContact(item.ModuleId, item.CreatedByUser, item.Name, item.Role, item.Email,
                                      item.Contact1, item.Contact2);
            }
            else
            {
                contactsDb.UpdateContact(item.ItemId, item.CreatedByUser, item.Name, item.Role, item.Email,
                                         item.Contact1, item.Contact2);
            }
        }

        #endregion
    }
}