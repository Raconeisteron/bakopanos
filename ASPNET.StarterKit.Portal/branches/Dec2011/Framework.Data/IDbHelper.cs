using System.Data;
using System.Data.Common;

namespace Framework.Data
{
    public interface IDbHelper
    {
        DbParameter CreateParameter(string parameterName, object value);

        DbParameter CreateOutputParameter(string parameterName, DbType type, int size);

        DataTable GetDataTable(string commandText, CommandType commandType, params DbParameter[] parameters);
        DataTable GetDataTable(string myCommandText, params DbParameter[] sqlParameters);

        DataRow GetDataRow(string commandText, CommandType commandType, params DbParameter[] sqlParameters);
        DataRow GetDataRow(string commandText, params DbParameter[] sqlParameters);

        T ExecuteNonQuery<T>(string commandText, DbParameter outParameter,
                             params DbParameter[] parameters);

        void ExecuteNonQuery(string commandText, params DbParameter[] parameters);
    }
}