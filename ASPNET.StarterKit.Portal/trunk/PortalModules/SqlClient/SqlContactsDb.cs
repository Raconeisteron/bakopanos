using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlContactsDb : Db, IContactsDb
    {
        private readonly string _connectionString;

        public SqlContactsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IContactsDb Members

        public IDataReader GetContacts(int moduleId)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetContacts", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleContact(int itemId)
        {


            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            
            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetSingleContact", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteContact(int itemID)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemID;
            
            //Execute method
            ExecuteNonQuery("Portal_DeleteContact", CommandType.StoredProcedure, parameterItemId);
        }

        public int AddContact(int moduleId, string userName, string name, string role, string email,
                              string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Direction = ParameterDirection.Output;
            
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            
            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            
            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            
            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            
            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;

            //Execute method
            ExecuteNonQuery("Portal_AddContact", CommandType.StoredProcedure,
                parameterItemId,
                parameterModuleId,
                parameterUserName,
                parameterName,
                parameterRole,
                parameterEmail,
                parameterContact1,
                parameterContact2
                );

            return (int)parameterItemId.Value;
        }

        public void UpdateContact(int itemId, string userName, string name, string role, string email,
                                  string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }


            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
           
            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            
            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            
            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            
            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            
            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;

            //Execute method
            ExecuteNonQuery("Portal_UpdateContact", CommandType.StoredProcedure,
                parameterItemID, 
                parameterUserName, 
                parameterName,
                parameterRole,
                parameterEmail, 
                parameterContact1, 
                parameterContact2);
        }

        #endregion
    }
}