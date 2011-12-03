using System;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IEventService
    {
        [OperationContract]
        Collection<PortalEvent> GetEvents(int moduleId);

        [OperationContract]
        PortalEvent GetSingleEvent(int itemId);

        [OperationContract]
        void DeleteEvent(int itemId);

        [OperationContract]
        int AddEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                     string description, string wherewhen);

        [OperationContract]
        void UpdateEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                         string description, string wherewhen);
    }
}