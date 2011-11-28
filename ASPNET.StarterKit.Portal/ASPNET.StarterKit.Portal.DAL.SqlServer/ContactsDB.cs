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

        //*********************************************************************
        //
        // GetContacts Method
        //
        // The GetContacts method returns a DataSet containing all of the
        // contacts for a specific portal module from the contacts
        // database.
        //
        // NOTE: A DataSet is returned from this method to allow this method to support
        // both desktop and mobile Web UI.
        //
        // Other relevant sources:
        //     + <a href="GetContacts.htm" style="color:green">GetContacts Stored Procedure</a>
        //
        //*********************************************************************

        #region IContactsDb Members

        public DataSet GetContacts(int moduleId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlDataAdapter("Portal_GetContacts", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.SelectCommand.AddParameterModuleId(moduleId);


            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        //*********************************************************************
        //
        // GetSingleContact Method
        //
        // The GetSingleContact method returns a IDataReader containing details
        // about a specific contact from the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleContact.htm" style="color:green">GetSingleContact Stored Procedure</a>
        //
        //*********************************************************************

        public IDataReader GetSingleContact(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_GetSingleContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);

            // Execute the command
            myConnection.Open();
            IDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        //*********************************************************************
        //
        // DeleteContact Method
        //
        // The DeleteContact method deletes the specified contact from
        // the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="DeleteContact.htm" style="color:green">DeleteContact Stored Procedure</a>
        //
        //*********************************************************************

        public void DeleteContact(int itemID)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        //*********************************************************************
        //
        // AddContact Method
        //
        // The AddContact method adds a new contact to the Contacts
        // database table, and returns the ItemId value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddContact.htm" style="color:green">AddContact Stored Procedure</a>
        //
        //*********************************************************************

        public int AddContact(int moduleId, int itemId, String userName, String name, String role, String email,
                              String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_AddContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = myCommand.AddParameterItemId();
            myCommand.AddParameterModuleId(moduleId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterName(name);
            myCommand.AddParameterRole(role);
            myCommand.AddParameterEmail(email);
            myCommand.AddParameterContact1(contact1);
            myCommand.AddParameterContact2(contact2);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int) parameterItemID.Value;
        }

        //*********************************************************************
        //
        // UpdateContact Method
        //
        // The UpdateContact method updates the specified contact within
        // the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateContact.htm" style="color:green">UpdateContact Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateContact(int moduleId, int itemId, String userName, String name, String role, String email,
                                  String contact1, String contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_UpdateContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.AddParameterItemId(itemId);
            myCommand.AddParameterUserName(userName);
            myCommand.AddParameterName(name);
            myCommand.AddParameterRole(role);
            myCommand.AddParameterEmail(email);
            myCommand.AddParameterContact1(contact1);
            myCommand.AddParameterContact2(contact2);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        #endregion
    }
}