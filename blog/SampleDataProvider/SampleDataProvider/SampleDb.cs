using System;
using System.Data;

namespace DotNetDataProviderTemplate
{
    /// <summary>This class provides database-like operations to simulate a real
    /// data source. The class generates sample data and uses a fixed set of commands.
    /// </summary>
    internal class SampleDb
    {
        private const string _selectCmd = "select ";
        private const string _updateCmd = "update ";
        private SampleDbResultSet _resultset = null;
        
        /// <summary>
        /// The sample code simulates SELECT and UPDATE operations.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="resultset"></param>
        public void Execute(string cmd, out SampleDbResultSet resultset)
        {            
            if (0 == String.Compare(cmd, 0, _selectCmd, 0, _selectCmd.Length, true))
            {
                executeSelect(out resultset);
            }
            else if (0 == String.Compare(cmd, 0, _updateCmd, 0, _updateCmd.Length, true))
            {
                executeUpdate(out resultset);
            }
            else
            {
                throw new NotSupportedException("Command string was not recognized");
            }
        }

        private void executeSelect(out SampleDbResultSet resultset)
        {
            // If no sample data exists, create it.
            if (_resultset == null)
            {
                resultsetCreate();
            }
            // Return the sample results.
            resultset = _resultset;
        }

        private void executeUpdate(out SampleDbResultSet resultset)
        {
            // If no sample data exists, create it.
            if (_resultset == null)
            {
                resultsetCreate();
            }
            // Change a row to simulate an update command.
            _resultset.data[2, 2] = 4199;

            // Create a result set object that is empty except for the RecordsAffected field.
            resultset = new SampleDbResultSet();
            resultset.recordsAffected = 1;
        }

        private void resultsetCreate()
        {
            _resultset = new SampleDbResultSet();

            // RecordsAffected is always a zero value for a SELECT.
            _resultset.recordsAffected = 0;

            const int numCols = 3;
            _resultset.metaData = new SampleDbResultSet.MetaData[numCols];
            resultsetFillColumn(0, "id", typeof(Int32), 0);
            resultsetFillColumn(1, "name", typeof(String), 64);
            resultsetFillColumn(2, "orderid", typeof(Int32), 0);

            _resultset.data = new object[5, numCols];
            resultsetFillRow(0, 1, "Biggs", 2001);
            resultsetFillRow(1, 2, "Brown", 2121);
            resultsetFillRow(2, 3, "Jones", 2543);
            resultsetFillRow(3, 4, "Smith", 2772);
            resultsetFillRow(4, 5, "Tyler", 3521);
        }

        private void resultsetFillColumn(int idx, string name, Type type, int maxSize)
        {
            _resultset.metaData[idx].name = name;
            _resultset.metaData[idx].type = type;
            _resultset.metaData[idx].maxSize = maxSize;
        }

        private void resultsetFillRow(int idx, int id, string name, int orderid)
        {
            _resultset.data[idx, 0] = id;
            _resultset.data[idx, 1] = name;
            _resultset.data[idx, 2] = orderid;
        }
    }
}