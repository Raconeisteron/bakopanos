// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace UIComposition.Services
{
    internal class GetProjectsByIdParameterMapper : IParameterMapper
    {
        private readonly Database _db;

        public GetProjectsByIdParameterMapper(Database db)
        {
            _db = db;
        }

        #region IParameterMapper Members

        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            InitializeParameters(command);
            _db.SetParameterValue(command, "@EmployeeId", parameterValues[0]);
        }

        #endregion

        private void InitializeParameters(DbCommand command)
        {
            if (!command.Parameters.Contains("@EmployeeId"))
            {
                _db.AddInParameter(command, "@EmployeeId", DbType.Int32);
            }
        }
    }
}