using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UIComposition.Contracts;
using UIComposition.Services;

namespace UIComposition.ServiceHost
{
    public class DataService : IProjectService, IEmployeeService
    {
        public ObservableCollection<ProjectItem> RetrieveProjects(int employeeId)
        {
            return new FakeProjectService().RetrieveProjects(employeeId);
        }

        public ObservableCollection<EmployeeItem> RetrieveEmployees()
        {
            return new FakeEmployeeService().RetrieveEmployees();
        }
    }
}
