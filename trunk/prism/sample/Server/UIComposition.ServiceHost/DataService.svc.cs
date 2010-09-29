// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System;
using System.Collections.ObjectModel;
using UIComposition.Contracts;
using UIComposition.Services;

namespace UIComposition.ServiceHost
{
    public class DataService : IProjectService, IEmployeeService
    {
        static DataService()
        {
            AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
        }

        #region IEmployeeService Members

        public ObservableCollection<Employee> RetrieveEmployees()
        {
            return new EmployeeService().RetrieveEmployees();
        }

        #endregion

        #region IProjectService Members

        public ObservableCollection<Project> RetrieveProjects(int employeeId)
        {
            return new ProjectService().RetrieveProjects(employeeId);
        }

        #endregion
    }
}