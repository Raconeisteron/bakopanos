using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalEvent:PortalItem
    {
        public string Title { get; set; }
        public string WhereWhen { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}