using System;

namespace Portal.Modules.Service.Contracts
{
    public class PortalLink
    {
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate{ get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }
        public string MobileUrl { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ViewOrder { get; set; }
    }
}