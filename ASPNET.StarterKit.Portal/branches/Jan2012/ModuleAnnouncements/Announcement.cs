using System;

namespace ASPNET.StarterKit.Portal
{
    public class Announcement
    {
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}