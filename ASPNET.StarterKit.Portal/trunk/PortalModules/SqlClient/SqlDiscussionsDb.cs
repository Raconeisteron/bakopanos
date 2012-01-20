using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlDiscussionsDb : Db, IDiscussionsDb
    {
        private readonly string _connectionString;

        public SqlDiscussionsDb(string connectionString) :base(connectionString,"System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IDiscussionsDb Members

        public IDataReader GetTopLevelMessages(int moduleId)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetTopLevelMessages", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetThreadMessages(String parent)
        {
            
            // Add Parameters to SPROC
            var parameterParent = new SqlParameter("@Parent", SqlDbType.NVarChar, 750);
            parameterParent.Value = parent;

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetThreadMessages", CommandType.StoredProcedure, parameterParent);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleMessage(int itemId)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetSingleMessage", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public int AddMessage(int moduleId, int parentId, string userName, string title, string body)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Direction = ParameterDirection.Output;

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;

            var parameterBody = new SqlParameter("@Body", SqlDbType.NVarChar, 3000);
            parameterBody.Value = body;

            var parameterParentId = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            parameterParentId.Value = parentId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            //Execute method
            ExecuteNonQuery("Portal_AddMessage", CommandType.StoredProcedure,
                parameterItemId, 
                parameterTitle, 
                parameterBody, 
                parameterParentId, 
                parameterUserName, 
                parameterModuleId
                );

            return (int) parameterItemId.Value;
        }

        #endregion
    }
}