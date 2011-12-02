using System;
using System.Runtime.Serialization;

namespace Portal.Modules.Service
{
    [DataContract]
    public class PortalContact
    {
        [DataMember]
        public string Contact1 { get; set; }

        [DataMember]
        public string Contact2 { get; set; }

        [DataMember]
        public string CreatedByUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Role { get; set; }
    }
}