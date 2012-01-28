using System;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraEventsDb : IEventsDb
    {
        #region IEventsDb Members

        public Collection<PortalEvent> GetEvents(int moduleId)
        {
            throw new NotImplementedException();
        }

        public PortalEvent GetSingleEvent(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddEvent(int moduleId, string userName, string title, DateTime expireDate, string description,
                            string wherewhen)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(int itemId, string userName, string title, DateTime expireDate, string description,
                                string wherewhen)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}