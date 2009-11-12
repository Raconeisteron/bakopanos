using System;
using System.Data;
using System.Globalization;

namespace DotNetDataProviderTemplate
{
    public class TemplateDataReader : IDataReader
    {
        // The DataReader should always be open when returned to the user.
        private bool _open = true;

        // Keep track of the results and position
        // within the resultset (starts prior to first record).
        private SampleDbResultSet _resultset;
        private static int _startPos = -1;
        private int _pos = _startPos;

        /* 
         * Keep track of the connection in order to implement the
         * CommandBehavior.CloseConnection flag. A null reference means
         * normal behavior (do not automatically close).
         */
        private TemplateConnection _connection = null;

        /*
         * Because the user should not be able to directly create a 
         * DataReader object, the constructors are
         * marked as internal.
         */
        internal TemplateDataReader(SampleDbResultSet resultset)
        {
            _resultset = resultset;
        }

        internal TemplateDataReader(SampleDbResultSet resultset, TemplateConnection connection)
        {
            _resultset = resultset;
            _connection = connection;
        }

        /****
         * METHODS / PROPERTIES FROM IDataReader.
         ****/
        public int Depth
        {
            /*
             * Always return a value of zero if nesting is not supported.
             */
            get { return 0; }
        }

        public bool IsClosed
        {
            /*
             * Keep track of the reader state - some methods should be
             * disallowed if the reader is closed.
             */
            get { return !_open; }
        }

        public int RecordsAffected
        {
            /*
             * RecordsAffected is only applicable to batch statements
             * that include inserts/updates/deletes. The sample always
             * returns -1.
             */
            get { return -1; }
        }

        public void Close()
        {
            /*
             * Close the reader. The sample only changes the state,
             * but an actual implementation would also clean up any 
             * resources used by the operation. For example,
             * cleaning up any resources waiting for data to be
             * returned by the server.
             */
            _open = false;
        }

        public bool NextResult()
        {
            // The sample only returns a single resultset. However,
            // DbDataAdapter expects NextResult to return a value.
            return false;
        }

        public bool Read()
        {
            // Return true if it is possible to advance and if you are still positioned
            // on a valid row. Because the data array in the resultset
            // is two-dimensional, you must divide by the number of columns.
            if (++_pos >= _resultset.data.Length / _resultset.metaData.Length)
                return false;
            else
                return true;
        }

        public DataTable GetSchemaTable()
        {
            //$
            throw new NotSupportedException();
        }

        /****
         * METHODS / PROPERTIES FROM IDataRecord.
         ****/
        public int FieldCount
        {
            // Return the count of the number of columns, which in
            // this case is the size of the column metadata
            // array.
            get { return _resultset.metaData.Length; }
        }

        public String GetName(int i)
        {
            return _resultset.metaData[i].name;
        }

        public String GetDataTypeName(int i)
        {
            /*
             * Usually this would return the name of the type
             * as used on the back end, for example 'smallint' or 'varchar'.
             * The sample returns the simple name of the .NET Framework type.
             */
            return _resultset.metaData[i].type.Name;
        }

        public Type GetFieldType(int i)
        {
            // Return the actual Type class for the data type.
            return _resultset.metaData[i].type;
        }

        public Object GetValue(int i)
        {
            return _resultset.data[_pos, i];
        }

        public int GetValues(object[] values)
        {
            int i = 0, j = 0;
            for (; i < values.Length && j < _resultset.metaData.Length; i++, j++)
            {
                values[i] = _resultset.data[_pos, j];
            }

            return i;
        }

        public int GetOrdinal(string name)
        {
            // Look for the ordinal of the column with the same name and return it.
            for (int i = 0; i < _resultset.metaData.Length; i++)
            {
                if (0 == _cultureAwareCompare(name, _resultset.metaData[i].name))
                {
                    return i;
                }
            }

            // Throw an exception if the ordinal cannot be found.
            throw new IndexOutOfRangeException("Could not find specified column in results");
        }

        public object this[int i]
        {
            get { return _resultset.data[_pos, i]; }
        }

        public object this[String name]
        {
            // Look up the ordinal and return 
            // the value at that position.
            get { return this[GetOrdinal(name)]; }
        }

        public bool GetBoolean(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (bool)_resultset.data[_pos, i];
        }

        public byte GetByte(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (byte)_resultset.data[_pos, i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            // The sample does not support this method.
            throw new NotSupportedException("GetBytes not supported.");
        }

        public char GetChar(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (char)_resultset.data[_pos, i];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            // The sample does not support this method.
            throw new NotSupportedException("GetChars not supported.");
        }

        public Guid GetGuid(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Guid)_resultset.data[_pos, i];
        }

        public Int16 GetInt16(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Int16)_resultset.data[_pos, i];
        }

        public Int32 GetInt32(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Int32)_resultset.data[_pos, i];
        }

        public Int64 GetInt64(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Int64)_resultset.data[_pos, i];
        }

        public float GetFloat(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (float)_resultset.data[_pos, i];
        }

        public double GetDouble(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (double)_resultset.data[_pos, i];
        }

        public String GetString(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (String)_resultset.data[_pos, i];
        }

        public Decimal GetDecimal(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Decimal)_resultset.data[_pos, i];
        }

        public DateTime GetDateTime(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
            */
            return (DateTime)_resultset.data[_pos, i];
        }

        public IDataReader GetData(int i)
        {
            /*
             * The sample code does not support this method. Normally,
             * this would be used to expose nested tables and
             * other hierarchical data.
             */
            throw new NotSupportedException("GetData not supported.");
        }

        public bool IsDBNull(int i)
        {
            return _resultset.data[_pos, i] == DBNull.Value;
        }

        /*
         * Implementation specific methods.
         */
        private int _cultureAwareCompare(string strA, string strB)
        {
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.IgnoreCase);
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    this.Close();
                }
                catch (Exception e)
                {
                    throw new SystemException("An exception of type " + e.GetType() +
                                              " was encountered while closing the TemplateDataReader.");
                }
            }
        }

    }
}