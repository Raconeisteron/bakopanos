// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using UIComposition.Contracts;

namespace UIComposition.Services
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly Database _db;

        public EmployeeService()
        {
            _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("Db");
        }

        #region IEmployeeService Members

        public ObservableCollection<Employee> RetrieveEmployees()
        {
            const string cmdText = "select * from employee";

            IRowMapper<Employee> rowMapper = MapBuilder<Employee>.MapAllProperties()
                .Map(p => p.EmployeeId).ToColumn("Id")
                .Build();

            DataAccessor<Employee> employeeAccessor = _db.CreateSqlStringAccessor(cmdText, rowMapper);

            return new ObservableCollection<Employee>(employeeAccessor.Execute());
        }

        #endregion
    }
}