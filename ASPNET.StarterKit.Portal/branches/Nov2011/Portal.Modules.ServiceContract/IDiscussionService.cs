using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IDiscussionService
    {
        [OperationContract]
        Collection<PortalDiscussion> GetTopLevelMessages(int moduleId);

        [OperationContract]
        Collection<PortalDiscussion> GetThreadMessages(string parent);

        [OperationContract]
        PortalDiscussion GetSingleMessage(int itemId);

        [OperationContract]
        int AddMessage(int moduleId, int parentId, string userName, string title, string body);
    }
}