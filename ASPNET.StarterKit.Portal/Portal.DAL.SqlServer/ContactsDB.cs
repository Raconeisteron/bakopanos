using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// contacts within the Portal database.
    /// </summary>
    internal class ContactsDb : DbHelper, IContactsDb
    {
        private readonly string _connectionString;

        public ContactsDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IContactsDb Members

        /// <summary>
        /// The GetContacts method returns a DataSet containing all of the
        /// contacts for a specific portal module from the contacts
        /// database.        
        /// </summary>        
        public IDataReader GetContacts(int moduleId)
        {
            return GetItems(_connectionString, "Portal_GetContacts", moduleId);
        }

        /// <summary>
        /// The GetSingleContact method returns a IDataReader containing details
        /// about a specific contact from the Contacts database table.
        /// </summary>        
        public IDataReader GetSingleContact(int itemId)
        {
            // Return the datareader 
            return GetSingleItem(_connectionString, "Portal_GetSingleContact", itemId);
        }

        /// <summary>
        /// The DeleteContact method deletes the specified contact from
        /// the Contacts database table.
        /// </summary>        
        public void DeleteContact(int itemId)
        {
            DeleteItem(_connectionString, "Portal_DeleteContact", itemId);
        }

        /// <summary>
        /// The AddContact method adds a new contact to the Contacts
        /// database table, and returns the ItemId value as a result.
        /// </summary>        
        public int AddContact(int moduleId, int itemId, string userName, string name, string role, string email,
                              string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            return CreateItem(_connectionString, "Portal_AddContact", 
                              InputModuleId(moduleId),
                              InputUserName(userName),
                              InputName(name),
                              InputRole(role),
                              InputEmail(email),
                              InputContact1(contact1),
                              InputContact2(contact2));
        }

        /// <summary>
        /// The UpdateContact method updates the specified contact within
        /// the Contacts database table.
        /// </summary>        
        public void UpdateContact(int moduleId, int itemId, string userName, string name, string role, string email,
                                  string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            ExecuteNonQuery(_connectionString, "Portal_UpdateContact",
                            InputItemId(itemId),
                            InputUserName(userName),
                            InputName(name),
                            InputRole(role),
                            InputEmail(email),
                            InputContact1(contact1),
                            InputContact2(contact2));

        }

        #endregion
    }
}