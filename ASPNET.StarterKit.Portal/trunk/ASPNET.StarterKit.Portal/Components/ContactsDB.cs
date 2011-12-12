using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// contacts within the Portal database.
    /// </summary>
    public class ContactsDb : DbHelper, IContactsDb
    {
        /// <returns>
        /// The GetContacts method returns a DataSet containing all of the
        /// contacts for a specific portal module from the contacts database.
        /// </returns>
        public DataTable GetContacts(int moduleId)
        {
            var parameterModuleId = CreateParameter("@ModuleID", moduleId);

            return GetDataTable("Portal_GetContacts", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleContact method returns a SqlDataReader containing details
        /// about a specific contact from the Contacts database table.
        /// </returns>
        public DataRow GetSingleContact(int itemId)
        {
            var parameterItemId = CreateParameter("@ItemID", itemId);

            return GetDataRow("Portal_GetSingleContact", parameterItemId);
        }


        /// <summary>
        /// The DeleteContact method deletes the specified contact from
        /// the Contacts database table.
        /// </summary>
        public void DeleteContact(int itemId)
        {
            var parameterItemId = CreateParameter("@ItemID", itemId);

            ExecuteNonQuery("Portal_DeleteContact", parameterItemId);
        }

        /// <summary>
        /// The AddContact method adds a new contact to the Contacts
        /// database table, and returns the ItemId value as a result.
        /// </summary>
        public int AddContact(int moduleId, String userName, String name, String role, String email,
                                     String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = CreateOutputParameter("@ItemID");
            var parameterModuleId = CreateParameter("@ModuleID", moduleId);
            var parameterUserName = CreateParameter("@UserName", userName);
            var parameterName = CreateParameter("@Name", name);
            var parameterRole = CreateParameter("@Role", role);
            var parameterEmail = CreateParameter("@Email", email);
            var parameterContact1 = CreateParameter("@Contact1", contact1);
            var parameterContact2 = CreateParameter("@Contact2", contact2);

            return ExecuteNonQuery<int>("Portal_AddContact", parameterItemId, parameterModuleId, parameterUserName,
                                        parameterName, parameterRole, parameterEmail, parameterContact1,
                                        parameterContact2);
        }

        /// <summary>
        /// The UpdateContact method updates the specified contact within
        /// the Contacts database table.
        /// </summary>
        public void UpdateContact(int itemId, String userName, String name, String role, String email,
                                         String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = CreateParameter("@ItemID", itemId);
            var parameterUserName = CreateParameter("@UserName", userName);
            var parameterName = CreateParameter("@Name", name);
            var parameterRole = CreateParameter("@Role", role);
            var parameterEmail = CreateParameter("@Email", email);
            var parameterContact1 = CreateParameter("@Contact1", contact1);
            var parameterContact2 = CreateParameter("@Contact2", contact2);

            ExecuteNonQuery("Portal_UpdateContact", parameterItemId, parameterUserName, parameterName,
                            parameterRole, parameterEmail, parameterContact1, parameterContact2);
        }
    }
}