namespace Bakopanos.Samples.NW.Services
{
    public class Service1 : IService1
    {
        #region IService1 Members

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #endregion
    }
}