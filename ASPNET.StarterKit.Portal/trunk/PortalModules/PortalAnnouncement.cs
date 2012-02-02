using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalAnnouncement : PortalItem
    {
        public string Title { get; set; }
        public string MoreLink { get; set; }
        public string MobileMoreLink { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
    }
}