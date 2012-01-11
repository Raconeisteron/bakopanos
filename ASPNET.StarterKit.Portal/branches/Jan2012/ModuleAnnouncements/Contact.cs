using System;

namespace ASPNET.StarterKit.Portal
{
    public class Contact : PortalItem
    {
        public String CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Name { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }
        public String Contact1 { get; set; }
        public String Contact2 { get; set; }
    }
}