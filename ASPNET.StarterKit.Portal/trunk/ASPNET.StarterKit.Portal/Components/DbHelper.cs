using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public class DbHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        private static readonly string ProviderName =
            ConfigurationManager.ConnectionStrings["connectionString"].ProviderName;

        private static readonly DbProviderFactory Factory = DbProviderFactories.GetFactory(ProviderName);

        protected static DataTable GetDataTable(string myCommandText, params DbParameter[] sqlParameters)
        {
            foreach (DataTable table in GetDataSet(myCommandText, sqlParameters).Tables)
            {
                return table;
            }
            throw new ApplicationException("Command returned no result");
        }

        protected static DataRow GetDataRow(string myCommandText, params DbParameter[] sqlParameters)
        {
            foreach (DataRow row in GetDataTable(myCommandText, sqlParameters).Rows)
            {
                return row;
            }
            throw new ApplicationException("Command returned no result");
        }

        private static DataSet GetDataSet(string myCommandText, params DbParameter[] sqlParameters)
        {
            // Create Instance of Connection and Command Object
            DbConnection myConnection = Factory.CreateConnection();
            myConnection.ConnectionString = ConnectionString;
            DbDataAdapter myCommand = Factory.CreateDataAdapter();
            myCommand.SelectCommand = Factory.CreateCommand();
            myCommand.SelectCommand.CommandText = myCommandText;
            myCommand.SelectCommand.Connection = myConnection;
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.SelectCommand.Parameters.AddRange(sqlParameters);

            // Create and Fill the DataSet
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet);

            // Return the DataSet
            return myDataSet;
        }

        protected static T ExecuteNonQuery<T>(string myCommandText, DbParameter outSqlParameter,
                                              params DbParameter[] sqlParameters)
        {
            var parameters = new List<DbParameter> {outSqlParameter};
            parameters.AddRange(sqlParameters);
            ExecuteNonQuery(myCommandText, parameters.ToArray());

            return (T) outSqlParameter.Value;
        }

        protected static void ExecuteNonQuery(string myCommandText, params DbParameter[] sqlParameters)
        {
            // Create Instance of Connection and Command Object
            DbConnection myConnection = Factory.CreateConnection();
            myConnection.ConnectionString = ConnectionString;
            DbCommand myCommand = Factory.CreateCommand();
            myCommand.CommandText = myCommandText;
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            myCommand.Parameters.AddRange(sqlParameters);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}