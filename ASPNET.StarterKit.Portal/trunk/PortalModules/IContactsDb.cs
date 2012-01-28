using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IContactsDb
    {
        Collection<PortalContact> GetContacts(int moduleId);
        PortalContact GetSingleContact(int itemId);
        void DeleteContact(int itemId);

        int AddContact(int moduleId, string userName, string name, string role, string email,
                       string contact1, string contact2);

        void UpdateContact(int itemId, string userName, string name, string role, string email,
                           string contact1, string contact2);
    }
}