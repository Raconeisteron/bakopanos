using System;

namespace PortalStarterKit.Model
{    
    public class TabDefinitionInfoAttribute : Attribute
    {
        private readonly string _tabDefId;

        public TabDefinitionInfoAttribute(string tabDefId, string friendlyName, string sourceFile)
        {
            _tabDefId = tabDefId;
            FriendlyName = friendlyName;
            SourceFile = sourceFile;
        }

        public Guid TabDefId
        {
            get { return Guid.Parse(_tabDefId); }
        }

        public string FriendlyName { get; private set; }
        public string SourceFile { get; private set; }
    }
    
    public class TabDefinition
    {
        public Guid TabDefId { get; set; }
        public string FriendlyName { get; set; }
        public string SourceFile { get; set; }
    }
}