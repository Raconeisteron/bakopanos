namespace ASPNET.StarterKit.Portal
{
    public class PortalLink : PortalItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string MobileUrl { get; set; }
        public int ViewOrder { get; set; }
        public string Description { get; set; }
    }
}