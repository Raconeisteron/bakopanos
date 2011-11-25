﻿using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL.SqlServer
{
    internal class PortalDB : IPortalDB
    {
        private readonly string _connectionString;

        public PortalDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region IPortalDB Members

        public void PortalDeleteModule(params int[] moduleIds)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(_connectionString);
            var myCommand = new SqlCommand("Portal_DeleteModule", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            myConnection.Open();

            foreach (int moduleId in moduleIds)
            {
                myCommand.Parameters.Clear();
                parameterModuleID.Value = moduleId;
                myCommand.Parameters.Add(parameterModuleID);

                // Open the database connection and execute the command
                myCommand.ExecuteNonQuery();
            }

            // Close the connection
            myConnection.Close();
        }

        #endregion
    }
}