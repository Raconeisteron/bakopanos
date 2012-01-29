using System.Collections.ObjectModel;
using ASPNET.StarterKit.Portal;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class ContactsViewModel : WorkspaceViewModel
    {
        
        public ContactsViewModel(IContactsDb contactsDb, ITabConfigurationDb tabConfigurationDb)
        {
           
            base.DisplayName = Strings.ContactsViewModel_DisplayName;

            Contacts = new Collection<PortalContact>();

            foreach (TabStripDetails tab in tabConfigurationDb.FindDesktopTabs())
            {
                foreach (ModuleSettings module in tabConfigurationDb.FindModules(tab.TabId))
                {
                    foreach (PortalContact portalContact in contactsDb.GetContacts(module.ModuleId))
                    {
                        Contacts.Add(portalContact);
                    }
                }
            }
        }

        public Collection<PortalContact> Contacts { get; private set; }

    }
}