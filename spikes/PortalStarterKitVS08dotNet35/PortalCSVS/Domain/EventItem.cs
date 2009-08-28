using System;

namespace ASPNET.StarterKit.Portal
{
    public class EventItem
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
        public DateTime ExpireDate { get; set; }
        public String Description { get; set; }
        public String Wherewhen { get; set; }
    }
}