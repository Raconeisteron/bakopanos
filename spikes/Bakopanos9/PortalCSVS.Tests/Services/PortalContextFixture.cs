using System;
using System.Data.Services.Client;
using ASPNET.StarterKit.Portal.PortalContextServiceReference;
using NUnit.Framework;

namespace ASPNET.StarterKit.Portal
{
    [TestFixture]
    public class PortalContextFixture
    {
        private readonly PortalContext _proxy =
            new PortalContext(new Uri("http://localhost:2873/Services/PortalWebDataService.svc/"));

        [Test, Explicit]
        public void SmokeTest()
        {
            DataServiceQuery<PortalAnnouncement> announcements = _proxy.Announcements;
            DataServiceQuery<PortalLink> links = _proxy.Links;
            DataServiceQuery<PortalContact> contacts = _proxy.Contacts;
            DataServiceQuery<PortalEvent> events = _proxy.Events;
        }
    }
}