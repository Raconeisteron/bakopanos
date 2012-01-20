using System.Data;
using System.Data.Common;
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
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetContacts", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleContact(int itemId)
        {


            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            
            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetSingleContact", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteContact(int itemID)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemID);

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
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterName = CreateParameter("@Name", name);
            DbParameter parameterRole = CreateParameter("@Role",role);
            DbParameter parameterEmail = CreateParameter("@Email", email);
            DbParameter parameterContact1 = CreateParameter("@Contact1", contact1);
            DbParameter parameterContact2 = CreateParameter("@Contact2", contact2);

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
            DbParameter parameterItemId = CreateParameter("@ItemID",itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterName = CreateParameter("@Name", name);
            DbParameter parameterRole = CreateParameter("@Role", role);
            DbParameter parameterEmail = CreateParameter("@Email", email);
            DbParameter parameterContact1 = CreateParameter("@Contact1", contact1);
            DbParameter parameterContact2 = CreateParameter("@Contact2", contact2);

            //Execute method
            ExecuteNonQuery("Portal_UpdateContact", CommandType.StoredProcedure,
                parameterItemId, 
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