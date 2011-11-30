using Portal.Modules.Contracts;
using Portal.Modules.DAL;

namespace Portal.Modules.Services
{
    internal class LinkService : ILinkService
    {
        #region ILinkService Members

        public void CreateOrUpdate(PortalLink item)
        {
            ILinksDb linksDb = ModulesDataAccess.LinkDb;
            if (item.ItemId == 0)
            {
                linksDb.AddLink(item.ModuleId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                item.ViewOrder, item.Description);
            }
            else
            {
                linksDb.UpdateLink(item.ItemId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                   item.ViewOrder, item.Description);
            }
        }

        #endregion
    }
}