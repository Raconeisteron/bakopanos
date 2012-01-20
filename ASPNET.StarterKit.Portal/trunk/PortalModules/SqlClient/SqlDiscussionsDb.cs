using System;
using System.Data;
using System.Data.Common;
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
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetTopLevelMessages", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetThreadMessages(String parent)
        {
            
            // Add Parameters to SPROC
            DbParameter parameterParent = CreateParameter("@Parent", parent);

            //Execute method and populate result
            IDataReader result = ExecuteReader("Portal_GetThreadMessages", CommandType.StoredProcedure, parameterParent);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleMessage(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID",itemId);

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
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            DbParameter parameterTitle = CreateParameter("@Title",  title);
            DbParameter parameterBody = CreateParameter("@Body",body);
            DbParameter parameterParentId = CreateParameter("@ParentID", parentId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

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