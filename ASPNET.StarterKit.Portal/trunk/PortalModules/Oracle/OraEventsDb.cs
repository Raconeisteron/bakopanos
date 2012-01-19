using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraEventsDb:IEventsDb
    {
        public IDataReader GetEvents(int moduleId)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetSingleEvent(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddEvent(int moduleId, string userName, string title, DateTime expireDate, string description, string wherewhen)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(int itemId, string userName, string title, DateTime expireDate, string description, string wherewhen)
        {
            throw new NotImplementedException();
        }
    }
}