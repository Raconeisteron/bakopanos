using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IEventsDb
    {
        DataSet GetEvents(int moduleId);
        IDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                     String description, String wherewhen);

        void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                         String description, String wherewhen);
    }
}