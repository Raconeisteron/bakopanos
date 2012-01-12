using System;

namespace Portal.Modules.Data
{
    public interface IContactDb : IDb
    {
        int AddContact(int moduleId, String userName, String name, String role, String email,
                       String contact1, String contact2);

        void UpdateContact(int itemId, String userName, String name, String role, String email,
                           String contact1, String contact2);
    }
}