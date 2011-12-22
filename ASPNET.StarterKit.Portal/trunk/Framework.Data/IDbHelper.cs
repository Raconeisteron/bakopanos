using System.Data;
using System.Data.Common;

namespace Framework.Data
{
    public interface IDbHelper
    {
        DbParameter CreateParameter(string parameterName, object value);
        DbParameter CreateOutputParameter(string parameterName);
        DataTable GetDataTable(string myCommandText, params DbParameter[] sqlParameters);
        DataRow GetDataRow(string myCommandText, params DbParameter[] sqlParameters);

        T ExecuteNonQuery<T>(string myCommandText, DbParameter outSqlParameter,
                             params DbParameter[] sqlParameters);

        void ExecuteNonQuery(string myCommandText, params DbParameter[] sqlParameters);
    }
}