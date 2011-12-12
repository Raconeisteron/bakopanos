using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IContactsDb
    {
        /// <returns>
        /// The GetContacts method returns a DataSet containing all of the
        /// contacts for a specific portal module from the contacts database.
        /// </returns>
        DataTable GetContacts(int moduleId);

        /// <returns>
        /// The GetSingleContact method returns a SqlDataReader containing details
        /// about a specific contact from the Contacts database table.
        /// </returns>
        DataRow GetSingleContact(int itemId);

        /// <summary>
        /// The DeleteContact method deletes the specified contact from
        /// the Contacts database table.
        /// </summary>
        void DeleteContact(int itemId);

        /// <summary>
        /// The AddContact method adds a new contact to the Contacts
        /// database table, and returns the ItemId value as a result.
        /// </summary>
        int AddContact(int moduleId, String userName, String name, String role, String email,
                                       String contact1, String contact2);

        /// <summary>
        /// The UpdateContact method updates the specified contact within
        /// the Contacts database table.
        /// </summary>
        void UpdateContact(int itemId, String userName, String name, String role, String email,
                                           String contact1, String contact2);
    }
}