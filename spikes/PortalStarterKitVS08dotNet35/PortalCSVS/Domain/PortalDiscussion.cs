using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalDiscussion
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string Title { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Body { get; set; }

        public string DisplayOrder { get; set; }

        public PortalUser CreatedByUser { get; set; }

    }
}