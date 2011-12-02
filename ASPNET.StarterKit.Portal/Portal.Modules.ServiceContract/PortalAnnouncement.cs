using System;
using System.Runtime.Serialization;

namespace Portal.Modules.Service
{
    [DataContract]
    public class PortalAnnouncement
    {
        [DataMember]
        public string CreatedByUser { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime ExpireDate { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string MobileMoreLink { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public string MoreLink { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}