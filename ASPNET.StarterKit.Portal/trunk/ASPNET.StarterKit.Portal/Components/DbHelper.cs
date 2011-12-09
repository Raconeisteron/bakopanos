using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public class DbHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        protected static DataTable GetDataTable(string myCommandText, params SqlParameter[] sqlParameters)
        {
            foreach (DataTable table in GetDataSet(myCommandText, sqlParameters).Tables)
            {
                return table;
            }
            throw new ApplicationException("Command returned no result");
        }

        protected static DataRow GetDataRow(string myCommandText, params SqlParameter[] sqlParameters)
        {
            foreach (DataRow row in GetDataTable(myCommandText,sqlParameters).Rows)
            {
                return row;
            }
            throw new ApplicationException("Command returned no result");
        }

        private static DataSet GetDataSet(string myCommandText, params SqlParameter[] sqlParameters)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlDataAdapter(myCommandText, myConnection)
                                {
                                    SelectCommand = {CommandType = CommandType.StoredProcedure}
                                };

            // Add Parameters to SPROC
            myCommand.SelectCommand.Parameters.AddRange(sqlParameters);

            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        protected static T ExecuteNonQuery<T>(string myCommandText, SqlParameter outSqlParameter, params SqlParameter[] sqlParameters)
        {
            var parameters = new List<SqlParameter> {outSqlParameter};
            parameters.AddRange(sqlParameters);
            ExecuteNonQuery(myCommandText, parameters.ToArray());

            return (T)outSqlParameter.Value;
        }

        protected static void ExecuteNonQuery(string myCommandText, params SqlParameter[] sqlParameters)
        {
            // Create Instance of Connection and Command Object
            var myConnection = new SqlConnection(ConnectionString);
            var myCommand = new SqlCommand(myCommandText, myConnection) {CommandType = CommandType.StoredProcedure};

            // Add Parameters to SPROC
            myCommand.Parameters.AddRange(sqlParameters);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }


    }
}