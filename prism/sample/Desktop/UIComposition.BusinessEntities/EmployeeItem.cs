// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
namespace UIComposition.BusinessEntities
{
    public class EmployeeItem
    {
        public EmployeeItem(int employeeId)
        {
            EmployeeId = employeeId;
        }

        public int EmployeeId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}