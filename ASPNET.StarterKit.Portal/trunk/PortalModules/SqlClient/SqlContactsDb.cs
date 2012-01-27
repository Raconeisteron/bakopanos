using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlContactsDb : Db, IContactsDb
    {
        public SqlContactsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IContactsDb Members

        public Collection<PortalContact> GetContacts(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            //Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetContacts", CommandType.StoredProcedure, parameterModuleId);

            var contactList = new Collection<PortalContact>();

            while (reader.Read())
                contactList.Add(reader.ToPortalContact());

            // Return list
            return contactList;
        }

        public PortalContact GetSingleContact(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute method and populate reader
            IDataReader reader = ExecuteReader("Portal_GetSingleContact", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            PortalContact contact;
            if (reader.Read())
                contact = reader.ToPortalContact(itemId);
            else
                return null;

            // Return the item
            return contact;
        }

        public void DeleteContact(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

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
            parameterItemId.Size = 4;
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterName = CreateParameter("@Name", name);
            DbParameter parameterRole = CreateParameter("@Role", role);
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

            return Convert.ToInt32(parameterItemId.Value);
        }

        public void UpdateContact(int itemId, string userName, string name, string role, string email,
                                  string contact1, string contact2)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }


            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
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