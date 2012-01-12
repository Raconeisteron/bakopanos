using System;
using Portal.Modules.Model;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditAnnouncements : PortalEditPage<Announcement>
    {
        protected override void Set(Announcement item)
        {
            TitleField.Text = item.Title;
            MoreLinkField.Text = item.MoreLink;
            MobileMoreField.Text = item.MobileMoreLink;
            DescriptionField.Text = item.Description;
            ExpireField.Text = item.ExpireDate.ToShortDateString();
            CreatedBy.Text = item.CreatedByUser;
            CreatedDate.Text = item.CreatedDate.ToShortDateString();
        }

        protected override Announcement Get()
        {
            var announcement = new Announcement
                                   {
                                       ItemId = ItemId,
                                       ModuleId = ModuleId,
                                       Title = TitleField.Text,
                                       CreatedByUser = Context.User.Identity.Name,
                                       ExpireDate = DateTime.Parse(ExpireField.Text),
                                       Description = DescriptionField.Text,
                                       MoreLink = MoreLinkField.Text,
                                       MobileMoreLink = MobileMoreField.Text
                                   };
            return announcement;
        }
    }
}