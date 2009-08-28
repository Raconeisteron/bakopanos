using System;

namespace ASPNET.StarterKit.Portal
{
    public class DiscussionItem
    {
        public int ModuleId { get; set; }
        public int ParentId { get; set; }
        public String UserName { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
    }
}