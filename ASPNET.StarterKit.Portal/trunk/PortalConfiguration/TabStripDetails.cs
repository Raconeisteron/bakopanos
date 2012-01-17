namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    /// -- for a specific Tab in the Portal
    /// </summary>
    public class TabStripDetails
    {
        public string AuthorizedRoles { get; set; }
        public int TabId { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
    }
}