using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalLink
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string MobileUrl { get; set; }

        public int? ViewOrder { get; set; }

        public string Description { get; set; }
    }
}