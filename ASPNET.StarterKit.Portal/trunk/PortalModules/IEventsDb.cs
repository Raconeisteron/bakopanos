using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IEventsDb
    {
        Collection<PortalEvent> GetEvents(int moduleId);
        PortalEvent GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, string userName, string title, DateTime expireDate,
                     string description, string wherewhen);

        void UpdateEvent(int itemId, string userName, string title, DateTime expireDate,
                         string description, string wherewhen);
    }
}