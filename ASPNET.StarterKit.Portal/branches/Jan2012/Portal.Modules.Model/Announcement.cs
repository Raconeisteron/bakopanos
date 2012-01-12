using System;

namespace Portal.Modules.Model
{
    public class Announcement : PortalItem
    {
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
        public string MoreLink { get; set; }
        public string MobileMoreLink { get; set; }
    }
}