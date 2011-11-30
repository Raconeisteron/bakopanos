using System.Data;

namespace Portal.Modules.Data
{
    public interface IContactsDb
    {
        IDataReader GetContacts(int moduleId);
        IDataReader GetSingleContact(int itemId);
        void DeleteContact(int itemId);

        int AddContact(int moduleId, string userName, string name, string role, string email,
                       string contact1, string contact2);

        void UpdateContact(int itemId, string userName, string name, string role, string email,
                           string contact1, string contact2);
    }
}