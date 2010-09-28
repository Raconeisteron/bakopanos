// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using UIComposition.BusinessEntities;

namespace UIComposition.Services.Employee
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly Database _db;

        public EmployeeService(Database db)
        {
            _db = db;
        }

        #region IEmployeeService Members

        public ObservableCollection<EmployeeItem> RetrieveEmployees()
        {
            const string cmdText = "select * from employee";

            IRowMapper<EmployeeItem> rowMapper = MapBuilder<EmployeeItem>.MapAllProperties()
                .Map(p => p.EmployeeId).ToColumn("Id")
                .Build();

            DataAccessor<EmployeeItem> employeeAccessor = _db.CreateSqlStringAccessor(cmdText, rowMapper);

            return new ObservableCollection<EmployeeItem>(employeeAccessor.Execute());
        }

        #endregion
    }
}