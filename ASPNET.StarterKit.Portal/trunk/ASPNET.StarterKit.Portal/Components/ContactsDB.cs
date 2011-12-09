using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
   /// <summary>
   /// Class that encapsulates all data logic necessary to add/query/delete
   /// contacts within the Portal database.
   /// </summary>
    public class ContactsDb : DbHelper
    {
        /// <returns>
        /// The GetContacts method returns a DataSet containing all of the
        /// contacts for a specific portal module from the contacts database.
        /// </returns>
        public static DataTable GetContacts(int moduleId)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            
            return GetDataTable("Portal_GetContacts", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleContact method returns a SqlDataReader containing details
        /// about a specific contact from the Contacts database table.
        /// </returns>
        public static DataRow GetSingleContact(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            
            return GetDataRow("Portal_GetSingleContact", parameterItemId);
        }

        
        /// <summary>
        /// The DeleteContact method deletes the specified contact from
        /// the Contacts database table.
        /// </summary>
        public static void DeleteContact(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            ExecuteNonQuery("Portal_DeleteContact", parameterItemId);
        }

        /// <summary>
        /// The AddContact method adds a new contact to the Contacts
        /// database table, and returns the ItemId value as a result.
        /// </summary>
        public static int AddContact(int moduleId, String userName, String name, String role, String email,
                              String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {Value = name};
            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100) {Value = role};
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100) {Value = contact1};
            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100) {Value = contact2};

            return ExecuteNonQuery<int>("Portal_AddContact", parameterItemId, parameterModuleId, parameterUserName,
                parameterName, parameterRole, parameterEmail, parameterContact1, parameterContact2);
        }

        /// <summary>
        /// The UpdateContact method updates the specified contact within
        /// the Contacts database table.
        /// </summary>
        public static void UpdateContact(int itemId, String userName, String name, String role, String email,
                                  String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100) {Value = name};
            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100) {Value = role};
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100) {Value = email};
            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100) {Value = contact1};
            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100) {Value = contact2};

            ExecuteNonQuery("Portal_UpdateContact", parameterItemId, parameterUserName, parameterName,
                parameterRole, parameterEmail, parameterContact1, parameterContact2);

        }
    }
}