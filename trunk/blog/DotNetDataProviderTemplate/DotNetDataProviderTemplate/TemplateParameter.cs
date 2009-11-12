using System;
using System.Data;

namespace DotNetDataProviderTemplate
{
    public class TemplateParameter : IDataParameter
    {
        DbType _dbType = DbType.Object;
        ParameterDirection _direction = ParameterDirection.Input;
        bool _nullable = false;
        string _paramName;
        string _sourceColumn;
        DataRowVersion _sourceVersion = DataRowVersion.Current;
        object _value;

        public TemplateParameter()
        {
        }

        public TemplateParameter(string parameterName, DbType type)
        {
            _paramName = parameterName;
            _dbType = type;
        }

        public TemplateParameter(string parameterName, object value)
        {
            _paramName = parameterName;
            this.Value = value;
            // Setting the value also infers the type.
        }

        public TemplateParameter(string parameterName, DbType dbType, string sourceColumn)
        {
            _paramName = parameterName;
            _dbType = dbType;
            _sourceColumn = sourceColumn;
        }

        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Boolean IsNullable
        {
            get { return _nullable; }
        }

        public String ParameterName
        {
            get { return _paramName; }
            set { _paramName = value; }
        }

        public String SourceColumn
        {
            get { return _sourceColumn; }
            set { _sourceColumn = value; }
        }

        public DataRowVersion SourceVersion
        {
            get { return _sourceVersion; }
            set { _sourceVersion = value; }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _dbType = _inferType(value);
            }
        }

        private DbType _inferType(Object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Empty:
                    throw new SystemException("Invalid data type");

                case TypeCode.Object:
                    return DbType.Object;

                case TypeCode.DBNull:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    // Throw a SystemException for unsupported data types.
                    throw new SystemException("Invalid data type");

                case TypeCode.Boolean:
                    return DbType.Boolean;

                case TypeCode.Byte:
                    return DbType.Byte;

                case TypeCode.Int16:
                    return DbType.Int16;

                case TypeCode.Int32:
                    return DbType.Int32;

                case TypeCode.Int64:
                    return DbType.Int64;

                case TypeCode.Single:
                    return DbType.Single;

                case TypeCode.Double:
                    return DbType.Double;

                case TypeCode.Decimal:
                    return DbType.Decimal;

                case TypeCode.DateTime:
                    return DbType.DateTime;

                case TypeCode.String:
                    return DbType.String;

                default:
                    throw new SystemException("Value is of unknown data type");
            }
        }
    }
}