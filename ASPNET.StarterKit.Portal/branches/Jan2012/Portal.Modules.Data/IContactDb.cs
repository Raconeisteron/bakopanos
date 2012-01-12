namespace Portal.Modules.Data
{
    public interface IContactDb : IDb
    {
        int AddContact(int moduleId, string userName, string name, string role, string email,
                       string contact1, string contact2);

        void UpdateContact(int itemId, string userName, string name, string role, string email,
                           string contact1, string contact2);
    }
}