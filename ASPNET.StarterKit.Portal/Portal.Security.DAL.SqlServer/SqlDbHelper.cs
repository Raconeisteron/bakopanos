using System.Data;
using System.Data.SqlClient;

namespace Portal.Security.DAL.SqlServer
{
    internal class SqlDbHelper 
    {
        internal string ConnectionString { get; set; }

        protected void ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {            
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {                    
                    myCommand.CommandType = CommandType.StoredProcedure;                    
                    myCommand.Parameters.AddRange(parameters);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        protected int CreateItem(string commandText, SqlParameter returnValueParameter, params SqlParameter[] parameters)
        {            
            using (var myConnection = new SqlConnection(ConnectionString))
            {
                using (var myCommand = new SqlCommand(commandText, myConnection))
                {                    
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(returnValueParameter);
                    myCommand.Parameters.AddRange(parameters);
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            return (int)returnValueParameter.Value;
        }

        protected IDataReader GetItems(string commandText, params SqlParameter[] parameters)
        {            
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddRange(parameters);
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        protected IDataReader GetSingleItem(string commandText, SqlParameter parameter)
        {
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(commandText, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add(parameter);
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }


        #region Parameters

        protected static SqlParameter InputPortalId(int portalId)
        {
            return new SqlParameter("@PortalID", SqlDbType.Int, 4) { Value = portalId };
        }

        protected static SqlParameter InputUserId(int userId)
        {
            return new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = userId };
        }

        protected static SqlParameter ReturnValueUserId()
        {
            return new SqlParameter("@UserID", SqlDbType.Int, 4) { Direction = ParameterDirection.ReturnValue };
        }

        protected static SqlParameter InputRoleId(int roleId)
        {
            return new SqlParameter("@RoleID", SqlDbType.Int, 4) { Value = roleId };
        }

        protected static SqlParameter ReturnValueRoleId()
        {
            return new SqlParameter("@RoleID", SqlDbType.Int, 4) { Direction = ParameterDirection.ReturnValue };
        }

        protected static SqlParameter InputName(string name)
        {
            return new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = name };
        }

        protected static SqlParameter InputPassword(string password)
        {
            return new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = password };
        }

        protected static SqlParameter InputRoleName(string roleName)
        {
            return new SqlParameter("@RoleName", SqlDbType.NVarChar, 50) { Value = roleName };
        }

        protected static SqlParameter InputEmail(string email)
        {
            return new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = email };
        }

        #endregion
        

    }
}