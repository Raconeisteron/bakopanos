using System;
using System.Collections.Generic;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data
{
    public class TabDefinitionService
    {
        public IEnumerable<TabDefinition> ReadTabDefinitions()
        {
            return new List<TabDefinition>
                       {
                           new TabDefinition
                               {
                                   TabDefId = Guid.Parse("46C4ACA1-8DC1-4ACF-A8C2-68ED15E82E87"),
                                   FriendlyName = "Default",
                                   SourceFile = "~/Default.aspx"                                   
                               }
                       };
        }
    }
}