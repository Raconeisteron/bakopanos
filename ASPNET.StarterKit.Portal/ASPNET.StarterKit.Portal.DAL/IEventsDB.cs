using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IEventsDb
    {
        DataSet GetEvents(int moduleId);
        IDataReader GetSingleEvent(int itemId);
        void DeleteEvent(int itemId);

        int AddEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                     string description, string wherewhen);

        void UpdateEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                         string description, string wherewhen);
    }
}