// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using UIComposition.BusinessEntities;
using UIComposition.Infrastructure;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Model
{
    internal class EmployeeWorkItem : IEmployeeWorkItem
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IProjectService _projectService;
        private ObservableCollection<EmployeeItem> _employees;
        private EmployeeItem _selectedEmployee;
        private ObservableCollection<ProjectItem> _selectedEmployeeProjects;

        public EmployeeWorkItem(IEventAggregator eventAggregator, IProjectService projectService,
                                IEmployeeService employeeService)
        {
            _eventAggregator = eventAggregator;
            _projectService = projectService;
            _employeeService = employeeService;
            ReadCommand = new DelegateCommand<object>(Read, CanRead);
        }

        #region IEmployeeWorkItem Members

        public DelegateCommand<object> ReadCommand { get; private set; }

        public ObservableCollection<EmployeeItem> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                PropertyChanged.OnPropertyChanged(this, "Employees");
            }
        }

        public EmployeeItem Employee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value) return;
                _selectedEmployee = value;
                Projects = _projectService.RetrieveProjects(Employee.EmployeeId);
                _eventAggregator.GetEvent<SelectedEmployeeEvent>().Publish(value);
                PropertyChanged.OnPropertyChanged(this, "Employee");
            }
        }

        public ObservableCollection<ProjectItem> Projects
        {
            get { return _selectedEmployeeProjects; }
            set
            {
                _selectedEmployeeProjects = value;
                PropertyChanged.OnPropertyChanged(this, "Projects");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Read(object arg)
        {
            Employees = _employeeService.RetrieveEmployees();
        }

        private static bool CanRead(object arg)
        {
            return true;
        }

       
    }
}