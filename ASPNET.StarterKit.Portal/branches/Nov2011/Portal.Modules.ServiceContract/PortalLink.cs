using System;
using System.Runtime.Serialization;

namespace Portal.Modules.Service
{
    [DataContract]
    public class PortalLink
    {
        [DataMember]
        public string CreatedByUser { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string MobileUrl { get; set; }

        [DataMember]
        public int ModuleId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int ViewOrder { get; set; }
    }
}