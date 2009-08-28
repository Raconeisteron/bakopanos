using System;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IEventsDb
    {
        DataSet GetEvents(int moduleId);
        DbDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemID);

        int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                     String description, String wherewhen);

        void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                         String description, String wherewhen);
    }
}