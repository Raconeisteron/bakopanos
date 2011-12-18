using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public class DbHelper : IDbHelper
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _factory;

        public DbHelper(ConnectionStringSettings connectionStringSettings)
        {
            _connectionString = connectionStringSettings.ConnectionString;
            _factory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
        }

        public DbParameter CreateParameter(string parameterName, object value)
        {
            DbParameter parameter = _factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            return parameter;
        }

        public DbParameter CreateOutputParameter(string parameterName)
        {
            DbParameter parameter = _factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public DataTable GetDataTable(string myCommandText, params DbParameter[] sqlParameters)
        {
            foreach (DataTable table in GetDataSet(myCommandText, sqlParameters).Tables)
            {
                return table;
            }
            throw new ApplicationException("Command returned no result");
        }

        public DataRow GetDataRow(string myCommandText, params DbParameter[] sqlParameters)
        {
            foreach (DataRow row in GetDataTable(myCommandText, sqlParameters).Rows)
            {
                return row;
            }
            throw new ApplicationException("Command returned no result");
        }

        private DataSet GetDataSet(string myCommandText, params DbParameter[] sqlParameters)
        {
            var myDataSet = new DataSet();

            // Create Instance of Connection and Command Object
            using (DbConnection myConnection = _factory.CreateConnection())
            {
                myConnection.ConnectionString = _connectionString;
                DbDataAdapter myCommand = _factory.CreateDataAdapter();
                myCommand.SelectCommand = _factory.CreateCommand();
                myCommand.SelectCommand.CommandText = myCommandText;
                myCommand.SelectCommand.Connection = myConnection;
                myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

                // Add Parameters to SPROC
                myCommand.SelectCommand.Parameters.AddRange(sqlParameters);

                // Create and Fill the DataSet                
                myCommand.Fill(myDataSet);
            }

            // Return the DataSet
            return myDataSet;
        }

        public T ExecuteNonQuery<T>(string myCommandText, DbParameter outSqlParameter,
                                              params DbParameter[] sqlParameters)
        {
            var parameters = new List<DbParameter> {outSqlParameter};
            parameters.AddRange(sqlParameters);
            ExecuteNonQuery(myCommandText, parameters.ToArray());

            return (T) outSqlParameter.Value;
        }

        public void ExecuteNonQuery(string myCommandText, params DbParameter[] sqlParameters)
        {
            // Create Instance of Connection and Command Object
            using (DbConnection myConnection = _factory.CreateConnection())
            {
                myConnection.ConnectionString = _connectionString;
                DbCommand myCommand = _factory.CreateCommand();
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
}