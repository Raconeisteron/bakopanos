using System;

namespace ASPNET.StarterKit.Portal
{
    public class Announcement:PortalItem
    {
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public String Description { get; set; }
        public String MoreLink { get; set; }
        public String MobileMoreLink { get; set; }
    }
}