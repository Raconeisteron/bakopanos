using System;

namespace ASPNET.StarterKit.Portal
{
    public class PortalDiscussion
    {
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Body { get; set; }
        public string DisplayOrder { get; set; }
        public string CreatedByUser { get; set; }
        
        //custom store procedure variables
        public int NextMessageId { get; set; } //Portal_GetSingleMessage
        public int PrevMessageId { get; set; } //Portal_GetSingleMessage
        public string Parent { get; set; } //Portal_GetTopLevelMessages
        public int ChildCount { get; set; } //Portal_GetTopLevelMessages
        public int Indent { get; set; } //Portal_GetThreadMessages
    }
}