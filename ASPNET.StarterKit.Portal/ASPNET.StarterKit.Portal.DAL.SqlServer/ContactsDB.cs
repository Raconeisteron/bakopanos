using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// contacts within the Portal database.
    /// </summary>
    internal class ContactsDb : IContactsDb
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
        public DataSet GetContacts(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetContacts", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.SelectCommand.Parameters.Add(SqlParameterHelper.InputModuleId(moduleId));


            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        /// <summary>
        /// The GetSingleContact method returns a IDataReader containing details
        /// about a specific contact from the Contacts database table.
        /// </summary>        
        public IDataReader GetSingleContact(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.Add(SqlParameterHelper.InputItemId(itemId));

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        /// <summary>
        /// The DeleteContact method deletes the specified contact from
        /// the Contacts database table.
        /// </summary>        
        public void DeleteContact(int itemId)
        {
            DbHelper.ExecuteNonQuery(_connectionString, "Portal_DeleteContact", SqlParameterHelper.InputItemId(itemId));
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

            SqlParameter parameterItemId = SqlParameterHelper.ReturnValueItemId();
            DbHelper.ExecuteNonQuery(_connectionString, "Portal_AddContact",
                parameterItemId,
            SqlParameterHelper.InputModuleId(moduleId),
            SqlParameterHelper.InputUserName(userName),
            SqlParameterHelper.InputName(name),
            SqlParameterHelper.InputRole(role),
            SqlParameterHelper.InputEmail(email),
            SqlParameterHelper.InputContact1(contact1),
            SqlParameterHelper.InputContact2(contact2));
            
            return (int) parameterItemId.Value;
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
            
            DbHelper.ExecuteNonQuery(_connectionString, "Portal_UpdateContact",
                SqlParameterHelper.InputItemId(itemId), 
                SqlParameterHelper.InputUserName(userName),
                SqlParameterHelper.InputName(name),
                SqlParameterHelper.InputRole(role),
            SqlParameterHelper.InputEmail(email),
            SqlParameterHelper.InputContact1(contact1),
            SqlParameterHelper.InputContact2(contact2));

        }

        #endregion
    }
}