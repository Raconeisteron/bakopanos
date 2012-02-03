namespace ASPNET.StarterKit.Portal
{
    public class PortalDocument : PortalItem
    {
        public string FileNameUrl { get; set; }
        public string FileFriendlyName { get; set; }
        public string Category { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public int ContentSize { get; set; }
    }
}