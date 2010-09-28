// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.Contracts;
using UIComposition.Services;

namespace UIComposition.ServiceHost
{
    public class DataService : IProjectService, IEmployeeService
    {
        #region IEmployeeService Members

        public ObservableCollection<Employee> RetrieveEmployees()
        {
            return new FakeEmployeeService().RetrieveEmployees();
        }

        #endregion

        #region IProjectService Members

        public ObservableCollection<Project> RetrieveProjects(int employeeId)
        {
            return new FakeProjectService().RetrieveProjects(employeeId);
        }

        #endregion
    }
}