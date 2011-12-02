using System;

namespace Portal.Modules.Service.Contracts
{
    public class PortalContact
    {
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}