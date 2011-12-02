using System;
using System.Collections.ObjectModel;
using Portal.Modules.Data;

namespace Portal.Modules.Service
{
    internal class LinkService : ILinkService
    {
        #region ILinkService Members

        public void CreateOrUpdate(PortalLink item)
        {
            ILinksDb db = ModulesDataAccess.LinkDb;
            if (item.ItemId == 0)
            {
                db.AddLink(item.ModuleId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                item.ViewOrder, item.Description);
            }
            else
            {
                db.UpdateLink(item.ItemId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                   item.ViewOrder, item.Description);
            }
        }

        public Collection<PortalLink> GetLinks(int moduleId)
        {
            ILinksDb linksDb = ModulesDataAccess.LinkDb;
            var reader = linksDb.GetLinks(moduleId);

            var items = new Collection<PortalLink>();

            while (reader.Read())
            {
                var item = new PortalLink
                               {
                                   ItemId = Convert.ToInt32(reader["ItemId"]),
                                   ModuleId = moduleId,
                                   Title = (string)reader["Title"],
                                   Description = (string) reader["Description"],
                                   Url = (string) reader["Url"],
                                   //MobileUrl = (string) reader["MobileUrl"],
                                   ViewOrder = Convert.ToInt32(reader["ViewOrder"]),
                                   CreatedByUser = (string) reader["CreatedByUser"],
                                   CreatedDate = (DateTime) reader["CreatedDate"]
                               };
                items.Add(item);
            }

            reader.Close();

            return items;
        }

        #endregion
    }
}