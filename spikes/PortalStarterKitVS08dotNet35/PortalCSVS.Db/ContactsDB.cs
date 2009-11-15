using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    //*********************************************************************
    //
    // ContactDB Class
    //
    // Class that encapsulates all data logic necessary to add/query/delete
    // contacts within the Portal database.
    //
    //*********************************************************************

    public class ContactsDb : IContactsDb
    {
        [Dependency]
        public IDatabaseConfiguration DatabaseConfiguration
        {
            private get;
            set;
        }

        [Dependency]
        public IUsersDb UsersDb
        {
            private get;
            set;
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
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlDataAdapter("Portal_GetContacts", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;
            myCommand.SelectCommand.Parameters.Add(parameterModuleId);

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
        // The GetSingleContact method returns a SqlDataReader containing details
        // about a specific contact from the Contacts database table.
        //
        // Other relevant sources:
        //     + <a href="GetSingleContact.htm" style="color:green">GetSingleContact Stored Procedure</a>
        //
        //*********************************************************************

        public PortalContact GetSingleContact(int itemId)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
            var myCommand = new SqlCommand("Portal_GetSingleContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;
            myCommand.Parameters.Add(parameterItemId);

            // Execute the command
            myConnection.Open();
            // Return the item
            var dr= myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                var item = new PortalContact();   
                item.Name = (String) dr["Name"];
                item.Role = (String) dr["Role"];
                item.Email = (String) dr["Email"];
                item.Contact1 = (String) dr["Contact1"];
                item.Contact2 = (String) dr["Contact2"];
                item.CreatedByUser = UsersDb.GetSingleUser((String) dr["CreatedByUser"]);
                item.CreatedDate = ((DateTime) dr["CreatedDate"]);
                return item;
            }
            return default(PortalContact);
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
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
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
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
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
            var myConnection = new SqlConnection(DatabaseConfiguration.ConnectionString);
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