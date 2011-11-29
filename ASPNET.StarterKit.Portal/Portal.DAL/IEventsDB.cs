using System;
using System.Data;

namespace Portal.Modules.DAL
{
    public interface IEventsDb
    {
        IDataReader GetEvents(int moduleId);
        IDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                     string description, string wherewhen);

        void UpdateEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                         string description, string wherewhen);
    }
}