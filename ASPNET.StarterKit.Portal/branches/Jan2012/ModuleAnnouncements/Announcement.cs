using System;

namespace ASPNET.StarterKit.Portal
{

    public class Contact:PortalItem
    {
        
    }

    public class PortalItem
    {
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
    }

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