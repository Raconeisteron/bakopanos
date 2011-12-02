using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IPortalService
    {
        [OperationContract]
        void DeleteModule(params int[] moduleIds);
    }
}