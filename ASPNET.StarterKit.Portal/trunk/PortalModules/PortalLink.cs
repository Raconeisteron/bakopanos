using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalLink
    {
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string MobileUrl { get; set; }
        public int ViewOrder { get; set; }
        public string Description { get; set; }
    }
}