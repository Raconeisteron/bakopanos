using System;

namespace Portal.Modules.Model
{
    public class Contact : PortalItem
    {
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
    }
}