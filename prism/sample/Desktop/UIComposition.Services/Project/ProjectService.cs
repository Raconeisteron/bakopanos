// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using UIComposition.BusinessEntities;

namespace UIComposition.Services.Project
{
    internal class ProjectService : IProjectService
    {
        private readonly Database _db;

        public ProjectService(Database db)
        {
            _db = db;
        }

        #region IProjectService Members

        public ObservableCollection<ProjectItem> RetrieveProjects(int employeeId)
        {
            const string cmdText =
                "select Project.* from Project inner join EmployeeProject on Project.ProjectName = EmployeeProject.ProjectName where EmployeeProject.EmployeeId = @EmployeeId";

            IRowMapper<ProjectItem> rowMapper = MapBuilder<ProjectItem>.MapAllProperties()
                .Map(p => p.ProjectName).ToColumn("ProjectName")
                .Build();

            DataAccessor<ProjectItem> employeeAccessor = _db.CreateSqlStringAccessor(cmdText,
                                                                                     new GetProjectsByIdParameterMapper(
                                                                                         _db), rowMapper);

            return new ObservableCollection<ProjectItem>(employeeAccessor.Execute(employeeId));
        }

        #endregion
    }
}