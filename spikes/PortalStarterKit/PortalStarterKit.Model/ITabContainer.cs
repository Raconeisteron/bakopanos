using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public interface ITabContainer
    {
        List<Tab> Tabs { get; }

        Tab NewTab();
    }
}