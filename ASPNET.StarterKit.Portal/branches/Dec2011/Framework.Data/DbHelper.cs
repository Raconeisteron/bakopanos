using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Framework.Data
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

        #region IDbHelper Members

        public DbParameter CreateParameter(string parameterName, object value)
        {
            DbParameter parameter = _factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            return parameter;
        }

        public DbParameter CreateOutputParameter(string parameterName, DbType type, int size)
        {
            DbParameter parameter = _factory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.Output;
            parameter.Size = size;
            parameter.DbType = type;
            return parameter;
        }

        public DataTable GetDataTable(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DataSet dataSet = GetDataSet(commandText, commandType, parameters);
            foreach (DataTable table in dataSet.Tables)
            {
                return table;
            }
            throw new ApplicationException("Command returned no result");
        }

        public DataTable GetDataTable(string commandText, params DbParameter[] parameters)
        {
            DataSet dataSet = GetDataSet(commandText, parameters);
            foreach (DataTable table in dataSet.Tables)
            {
                return table;
            }
            throw new ApplicationException("Command returned no result");
        }

        public DataRow GetDataRow(string commandText,CommandType commandType, params DbParameter[] parameters)
        {
            foreach (DataRow row in GetDataTable(commandText, parameters).Rows)
            {
                return row;
            }
            throw new ApplicationException("Command returned no result");
        }

        public DataRow GetDataRow(string commandText, params DbParameter[] parameters)
        {
            return GetDataRow(commandText, CommandType.StoredProcedure, parameters);
        }

        public T ExecuteNonQuery<T>(string commandText, DbParameter outParameter,
                                    params DbParameter[] sqlParameters)
        {
            var parameters = new List<DbParameter> {outParameter};
            parameters.AddRange(sqlParameters);
            ExecuteNonQuery(commandText, parameters.ToArray());

            return (T) outParameter.Value;
        }

        public void ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            // Create Instance of Connection and Command Object
            using (DbConnection connection = _factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                DbCommand myCommand = _factory.CreateCommand();
                myCommand.CommandText = commandText;
                myCommand.Connection = connection;
                myCommand.CommandType = CommandType.StoredProcedure;

                // Add Parameters to SPROC
                myCommand.Parameters.AddRange(parameters);

                connection.Open();
                myCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        #endregion

        private DataSet GetDataSet(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            var dataSet = new DataSet();

            // Create Instance of Connection and Command Object
            using (DbConnection connection = _factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                DbDataAdapter command = _factory.CreateDataAdapter();
                command.SelectCommand = _factory.CreateCommand();
                command.SelectCommand.CommandText = commandText;
                command.SelectCommand.Connection = connection;
                command.SelectCommand.CommandType = commandType;

                // Add Parameters to SPROC
                command.SelectCommand.Parameters.AddRange(parameters);

                // Create and Fill the DataSet                
                command.Fill(dataSet);
            }

            // Return the DataSet
            return dataSet;
        }

        private DataSet GetDataSet(string commandText, params DbParameter[] parameters)
        {
            return GetDataSet(commandText, CommandType.StoredProcedure, parameters);
        }
    }
}