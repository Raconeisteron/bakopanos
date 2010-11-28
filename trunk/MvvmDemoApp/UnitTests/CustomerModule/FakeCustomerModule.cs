namespace DemoApp
{
    public class FakeCustomerModule : ICustomerModule
    {
        #region ICustomerModule Members

        public string CustomerDataFile
        {
            get { return Constants.CUSTOMER_DATA_FILE; }
        }

        #endregion
    }
}