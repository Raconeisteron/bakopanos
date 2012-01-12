using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IEventsDb
    {
        IDataReader GetEvents(int moduleId);
        IDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, String userName, String title, DateTime expireDate,
                                     String description, String wherewhen);

        void UpdateEvent(int itemId, String userName, String title, DateTime expireDate,
                                         String description, String wherewhen);
    }
}