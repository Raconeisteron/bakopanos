using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Data
{
    public interface IEventsDB
    {
        DataSet GetEvents(int moduleId);
        SqlDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemID);

        int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                     String description, String wherewhen);

        void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                         String description, String wherewhen);
    }
}