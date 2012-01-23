using System;
using System.Collections.Generic;
using ASPNET.StarterKit.Portal.PortalDao;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraContactsDb : IContactsDb
    {
        #region IContactsDb Members

        public List<PortalContact> GetContacts(int moduleId)
        {
            throw new NotImplementedException();
        }

        public PortalContact GetSingleContact(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddContact(int moduleId, string userName, string name, string role, string email, string contact1,
                              string contact2)
        {
            throw new NotImplementedException();
        }

        public void UpdateContact(int itemId, string userName, string name, string role, string email, string contact1,
                                  string contact2)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}