using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Modules.Data
{
    public class ContactSqlClientDb : Db, IContactDb
    {
        #region IContactDb Members

        public override IDataReader GetList(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetContacts", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        public override IDataReader GetSingle(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }


        public override void Delete(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }


        public int AddContact(int moduleId, string userName, string name, string role, string email,
                              string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_AddContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterItemID);

            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemID.Value;
        }


        public void UpdateContact(int itemId, string userName, string name, string role, string email,
                                  string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_UpdateContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemId;
            myCommand.Parameters.Add(parameterItemID);

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            var parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            var parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            var parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            var parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            var parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}