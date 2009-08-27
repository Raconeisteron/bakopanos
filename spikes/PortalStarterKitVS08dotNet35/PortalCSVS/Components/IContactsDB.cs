using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public interface IContactsDB
    {
        DataSet GetContacts(int moduleId);
        DbDataReader GetSingleContact(int itemId);
        void DeleteContact(int itemID);

        int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
                                       String contact1, String contact2);

        void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
                                           String contact1, String contact2);
    }
}