using System;
using Portal.Modules.Model;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditContacts : PortalEditPage<Contact>
    {
        protected override void Set(Contact item)
        {
            NameField.Text = item.Name;
            RoleField.Text = item.Role;
            EmailField.Text = item.Email;
            Contact1Field.Text = item.Contact1;
            Contact2Field.Text = item.Contact2;
            CreatedBy.Text = item.CreatedByUser;
            CreatedDate.Text = item.CreatedDate.ToShortDateString();
        }

        protected override Contact Get()
        {
            var contact = new Contact
                              {
                                  ItemId = ItemId,
                                  ModuleId = ModuleId,
                                  Name = NameField.Text,
                                  CreatedByUser = Context.User.Identity.Name,
                                  CreatedDate = DateTime.Parse(CreatedDate.Text),
                                  Contact1 = Contact1Field.Text,
                                  Contact2 = Contact2Field.Text,
                                  Email = EmailField.Text,
                                  Role = RoleField.Text
                              };
            return contact;
        }
    }
}