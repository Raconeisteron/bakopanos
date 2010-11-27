namespace DemoApp
{
    public class FakeCustomerModule : ICustomerModule
    {
        public string CustomerDataFile
        {
            get { return Constants.CUSTOMER_DATA_FILE; }
        }
    }
}