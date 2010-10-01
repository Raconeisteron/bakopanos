// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UIComposition.BusinessEntities;
using UIComposition.Services.DataServiceReference;

namespace UIComposition.Services.Project
{
    internal class WcfProjectService : IProjectService
    {
        private readonly ProjectServiceClient _service;

        public WcfProjectService(ProjectServiceClient service)
        {
            _service = service;
        }

        #region IProjectService Members

        public ObservableCollection<ProjectItem> RetrieveProjects()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<ProjectItem> RetrieveProjects(int employeeId)
        {
            return new ObservableCollection<ProjectItem>(from item in _service.RetrieveProjects(employeeId)
                                                         select
                                                             new ProjectItem
                                                                 {ProjectName = item.ProjectName, Role = item.Role});
        }

        #endregion
    }
}