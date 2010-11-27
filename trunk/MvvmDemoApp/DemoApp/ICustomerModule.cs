using System.Configuration;

namespace DemoApp
{
    public interface ICustomerModule
    {
        [ConfigurationProperty(("customerDataFile"))]
        string CustomerDataFile { get; }
    }
}