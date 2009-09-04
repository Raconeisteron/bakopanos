using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalContact
    {
        public int ItemID { get; set; }

        public int ModuleID { get; set; }

        public PortalUser CreatedByUser { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Contact1 { get; set; }

        public string Contact2 { get; set; }
    }
}