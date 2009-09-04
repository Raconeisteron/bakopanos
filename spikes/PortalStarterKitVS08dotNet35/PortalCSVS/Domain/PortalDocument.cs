using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalDocument
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string FileNameUrl { get; set; }

        public string FileFriendlyName { get; set; }

        public string Category { get; set; }

        public Byte[] Content { get; set; }

        public string ContentType { get; set; }

        public int? ContentSize { get; set; }

    }

   
}