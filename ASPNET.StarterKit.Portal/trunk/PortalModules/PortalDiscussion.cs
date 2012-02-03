namespace ASPNET.StarterKit.Portal
{
    public class PortalDiscussion : PortalItem
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string DisplayOrder { get; set; }

        //custom store procedure variables
        public int? NextMessageId { get; set; } //Portal_GetSingleMessage
        public int? PrevMessageId { get; set; } //Portal_GetSingleMessage
        public string Parent { get; set; } //Portal_GetTopLevelMessages
        public int ChildCount { get; set; } //Portal_GetTopLevelMessages
        public int Indent { get; set; } //Portal_GetThreadMessages
    }
}