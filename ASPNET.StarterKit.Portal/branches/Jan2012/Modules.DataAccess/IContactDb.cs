using System;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Modules.Data
{
    public interface IContactDb
    {
        IDataReader GetContacts(int moduleId);
        SqlDataReader GetSingleContact(int itemId);
        void DeleteContact(int itemID);

        int AddContact(int moduleId, String userName, String name, String role, String email,
                                       String contact1, String contact2);

        void UpdateContact(int itemId, String userName, String name, String role, String email,
                                           String contact1, String contact2);
    }
}