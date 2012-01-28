using System;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IDiscussionsDb
    {
        Collection<PortalDiscussion> GetTopLevelMessages(int moduleId);
        Collection<PortalDiscussion> GetThreadMessages(String parent);
        PortalDiscussion GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, string userName, string title, string body);
    }
}