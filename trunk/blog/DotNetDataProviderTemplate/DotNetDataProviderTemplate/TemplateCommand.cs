using System;
using System.Data;

namespace DotNetDataProviderTemplate
{
    public class TemplateCommand : IDbCommand
    {
        TemplateConnection _connection;
        TemplateTransaction _txn;
        string _cmdText;
        UpdateRowSource _updatedRowSource = UpdateRowSource.None;
        TemplateParameterCollection _parameters = new TemplateParameterCollection();

        // Implement the default constructor here.
        public TemplateCommand()
        {
        }

        // Implement other constructors here.
        public TemplateCommand(string cmdText)
        {
            _cmdText = cmdText;
        }

        public TemplateCommand(string cmdText, TemplateConnection connection)
        {
            _cmdText = cmdText;
            _connection = connection;
        }

        public TemplateCommand(string cmdText, TemplateConnection connection, TemplateTransaction txn)
        {
            _cmdText = cmdText;
            _connection = connection;
            _txn = txn;
        }

        /****
         * IMPLEMENT THE REQUIRED PROPERTIES.
         ****/
        public string CommandText
        {
            get { return _cmdText; }
            set { _cmdText = value; }
        }

        public int CommandTimeout
        {
            /*
             * The sample does not support a command time-out. As a result,
             * for the get, zero is returned because zero indicates an indefinite
             * time-out period. For the set, throw an exception.
             */
            get { return 0; }
            set { if (value != 0) throw new NotSupportedException(); }
        }

        public CommandType CommandType
        {
            /*
             * The sample only supports CommandType.Text.
             */
            get { return CommandType.Text; }
            set { if (value != CommandType.Text) throw new NotSupportedException(); }
        }

        public IDbConnection Connection
        {
            /*
             * The user should be able to set or change the connection at 
             * any time.
             */
            get { return _connection; }
            set
            {
                /*
                 * The connection is associated with the transaction
                 * so set the transaction object to return a null reference if the connection 
                 * is reset.
                 */
                if (_connection != value)
                    this.Transaction = null;

                _connection = (TemplateConnection)value;
            }
        }

        public TemplateParameterCollection Parameters
        {
            get { return _parameters; }
        }

        IDataParameterCollection IDbCommand.Parameters
        {
            get { return _parameters; }
        }

        public IDbTransaction Transaction
        {
            /*
             * Set the transaction. Consider additional steps to ensure that the transaction
             * is compatible with the connection, because the two are usually linked.
             */
            get { return _txn; }
            set { _txn = (TemplateTransaction)value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return _updatedRowSource; }
            set { _updatedRowSource = value; }
        }

        /****
         * IMPLEMENT THE REQUIRED METHODS.
         ****/
        public void Cancel()
        {
            // The sample does not support canceling a command
            // once it has been initiated.
            throw new NotSupportedException();
        }

        public IDbDataParameter CreateParameter()
        {
            return (IDbDataParameter)(new TemplateParameter());
        }

        public int ExecuteNonQuery()
        {
            /*
             * ExecuteNonQuery is intended for commands that do
             * not return results, instead returning only the number
             * of records affected.
             */

            // There must be a valid and open connection.
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection must valid and open");

            // Execute the command.
            SampleDbResultSet resultset;
            _connection.SampleDb.Execute(_cmdText, out resultset);

            // Return the number of records affected.
            return resultset.recordsAffected;
        }

        public IDataReader ExecuteReader()
        {
            /*
             * ExecuteReader should retrieve results from the data source
             * and return a DataReader that allows the user to process 
             * the results.
             */
            // There must be a valid and open connection.
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection must valid and open");

            // Execute the command.
            SampleDbResultSet resultset;
            _connection.SampleDb.Execute(_cmdText, out resultset);

            return new TemplateDataReader(resultset);
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            /*
             * ExecuteReader should retrieve results from the data source
             * and return a DataReader that allows the user to process 
             * the results.
             */

            // There must be a valid and open connection.
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection must valid and open");

            // Execute the command.
            SampleDbResultSet resultset;
            _connection.SampleDb.Execute(_cmdText, out resultset);

            /*
             * The only CommandBehavior option supported by this
             * sample is the automatic closing of the connection
             * when the user is done with the reader.
             */
            if (behavior == CommandBehavior.CloseConnection)
                return new TemplateDataReader(resultset, _connection);
            else
                return new TemplateDataReader(resultset);
        }

        public object ExecuteScalar()
        {
            /*
             * ExecuteScalar assumes that the command will return a single
             * row with a single column, or if more rows/columns are returned
             * it will return the first column of the first row.
             */

            // There must be a valid and open connection.
            if (_connection == null || _connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection must valid and open");

            // Execute the command.
            SampleDbResultSet resultset;
            _connection.SampleDb.Execute(_cmdText, out resultset);

            // Return the first column of the first row.
            // Return a null reference if there is no data.
            if (resultset.data.Length == 0)
                return null;

            return resultset.data[0, 0];
        }

        public void Prepare()
        {
            // The sample Prepare is a no-op.
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            /*
             * Dispose of the object and perform any cleanup.
             */
        }

    }
}