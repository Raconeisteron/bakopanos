using System;

namespace ASPNET.StarterKit.Portal
{
    public class LinkItem
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public String MobileUrl { get; set; }
        public int ViewOrder { get; set; }
        public String Description { get; set; }
    }
}