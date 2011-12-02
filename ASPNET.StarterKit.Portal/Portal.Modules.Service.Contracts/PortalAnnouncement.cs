using System;

namespace Portal.Modules.Service.Contracts
{
    public class PortalAnnouncement
    {
        public string CreatedByUser { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ItemId { get; set; }
        public string MobileMoreLink { get; set; }
        public int ModuleId { get; set; }
        public string MoreLink { get; set; }
        public string Title { get; set; }
    }
}