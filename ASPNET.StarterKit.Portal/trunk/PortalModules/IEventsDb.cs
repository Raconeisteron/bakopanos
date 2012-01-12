using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IEventsDb
    {
        IDataReader GetEvents(int moduleId);
        IDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, string userName, string title, DateTime expireDate,
                     string description, string wherewhen);

        void UpdateEvent(int itemId, string userName, string title, DateTime expireDate,
                         string description, string wherewhen);
    }
}