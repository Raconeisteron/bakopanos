using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IContactsDb
    {
        DataSet GetContacts(int moduleId);
        ContactItem GetSingleContact(int itemId);
        void DeleteContact(int itemID);

        int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
                       String contact1, String contact2);

        void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
                           String contact1, String contact2);
    }
}