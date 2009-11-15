using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalAnnouncement
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public PortalUser CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Title { get; set; }

        public string MoreLink { get; set; }

        public string MobileMoreLink { get; set; }

        public DateTime? ExpireDate { get; set; }

        public string Description { get; set; }

    }  
}