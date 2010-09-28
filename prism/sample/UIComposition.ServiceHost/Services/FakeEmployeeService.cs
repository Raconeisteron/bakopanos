// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using UIComposition.Contracts;

namespace UIComposition.Services
{
    internal class FakeEmployeeService : IEmployeeService
    {
        #region IEmployeeService Members

        public ObservableCollection<Employee> RetrieveEmployees()
        {
            var employees = new ObservableCollection<Employee>
                                {
                                    new Employee
                                        {
                                            EmployeeId = 1,
                                            FirstName = "John",
                                            LastName = "Smith",
                                            Phone = "+1 (425) 555-0101",
                                            Email = "john.smith@example.com",
                                            Address = "One Microsoft Way",
                                            City = "Redmond",
                                            State = "WA"
                                        },
                                    new Employee
                                        {
                                            EmployeeId = 2,
                                            FirstName = "Bonnie",
                                            LastName = "Skelly",
                                            Phone = "+1 (425) 555-0105",
                                            Email = "bonnie.skelly@example.com",
                                            Address = "One Microsoft Way",
                                            City = "Redmond",
                                            State = "WA"
                                        }
                                };

            return employees;
        }

        #endregion
    }
}