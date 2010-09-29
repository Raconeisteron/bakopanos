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
    internal class ProjectService : IProjectService
    {
        private readonly Database _db;

        public ProjectService()
        {
            _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("Db");
        }

        #region IProjectService Members

        public ObservableCollection<Project> RetrieveProjects(int employeeId)
        {
            const string cmdText =
                "select Project.* from Project inner join EmployeeProject on Project.ProjectName = EmployeeProject.ProjectName where EmployeeProject.EmployeeId = @EmployeeId";

            IRowMapper<Project> rowMapper = MapBuilder<Project>.MapAllProperties()
                .Map(p => p.ProjectName).ToColumn("ProjectName")
                .Build();

            DataAccessor<Project> employeeAccessor = _db.CreateSqlStringAccessor(cmdText,
                                                                                     new GetProjectsByIdParameterMapper(
                                                                                         _db), rowMapper);

            return new ObservableCollection<Project>(employeeAccessor.Execute(employeeId));
        }

        #endregion
    }
}