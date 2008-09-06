using System.Runtime.Serialization;
using System.ServiceModel;

namespace Bakopanos.Samples.NW.Services
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);     
    }
}