using System.Data;

namespace Portal.Modules.DAL
{
    public interface IContactsDb
    {
        IDataReader GetContacts(int moduleId);
        IDataReader GetSingleContact(int itemId);
        void DeleteContact(int itemId);

        int AddContact(int moduleId, int itemId, string userName, string name, string role, string email,
                       string contact1, string contact2);

        void UpdateContact(int moduleId, int itemId, string userName, string name, string role, string email,
                           string contact1, string contact2);
    }
}