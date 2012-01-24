using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IDiscussionsDb
    {
        List<PortalDiscussion> GetTopLevelMessages(int moduleId);
        List<PortalDiscussion> GetThreadMessages(String parent);
        PortalDiscussion GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, string userName, string title, string body);
    }
}