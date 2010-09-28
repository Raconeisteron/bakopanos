// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Events;
using UIComposition.BusinessEntities;
using UIComposition.Infrastructure;

namespace UIComposition.Services.Employee
{
    internal class SelectedEmployeeWorkItem : ISelectedEmployeeWorkItem
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProjectService _projectService;
        private EmployeeItem _selectedEmployee;
        private ObservableCollection<ProjectItem> _selectedEmployeeProjects;

        public SelectedEmployeeWorkItem(IEventAggregator eventAggregator, IProjectService projectService)
        {
            _eventAggregator = eventAggregator;
            _projectService = projectService;
        }

        #region ISelectedEmployeeWorkItem Members

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
    }
}