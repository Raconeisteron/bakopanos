using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalEvent
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public PortalUser CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Title { get; set; }
        
        public string WhereWhen { get; set; }

        public string Description { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}