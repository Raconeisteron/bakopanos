// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.Linq;
using UIComposition.BusinessEntities;
using UIComposition.Services.DataServiceReference;

namespace UIComposition.Services.Employee
{
    internal class WcfEmployeeService : IEmployeeService
    {
        private readonly EmployeeServiceClient _service;

        public WcfEmployeeService(EmployeeServiceClient service)
        {
            _service = service;
        }

        #region IEmployeeService Members

        public ObservableCollection<EmployeeItem> RetrieveEmployees()
        {
            return new ObservableCollection<EmployeeItem>(from item in _service.RetrieveEmployees()
                                                          select
                                                              new EmployeeItem
                                                                  {
                                                                      EmployeeId = item.EmployeeId,
                                                                      LastName = item.LastName,
                                                                      FirstName = item.FirstName,
                                                                      Address = item.Address,
                                                                      City = item.City,
                                                                      Email = item.Email,
                                                                      Phone = item.Phone,
                                                                      State = item.State
                                                                  });
        }

        #endregion
    }
}