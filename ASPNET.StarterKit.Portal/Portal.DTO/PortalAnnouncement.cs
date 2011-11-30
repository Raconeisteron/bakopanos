using System;

namespace Portal.Contracts
{
    public class PortalAnnouncement
    {
        public int ItemId;
        public int ModuleId;
        public string CreatedByUser;        
        public string Title;
        public string MoreLink;
        public string MobileMoreLink;
        public DateTime ExpireDate;
        public string Description;
    }
}
