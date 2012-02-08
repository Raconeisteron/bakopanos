using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    //All the switch cases used in conjuction with SPROC field names retrieval (record.GetName) are *Case Sensitive*.
    public static class PortalModulesExtensions
    {
        public static PortalAnnouncement ToPortalAnnouncement(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalAnnouncement();

            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "Title":
                        item.Title = Convert.ToString(record["Title"]);
                        break;
                    case "MoreLink":
                        item.MoreLink = Convert.ToString(record["MoreLink"]);
                        break;
                    case "MobileMoreLink":
                        item.MobileMoreLink = Convert.ToString(record["MobileMoreLink"]);
                        break;
                    case "ExpireDate":
                        item.ExpireDate = Convert.ToDateTime(record["ExpireDate"]);
                        break;
                    case "Description":
                        item.Description = Convert.ToString(record["Description"]);
                        break;
                }
            }

            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalContact ToPortalContact(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalContact();

            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "Name":
                        item.Name = Convert.ToString(record["Name"]);
                        break;
                    case "Role":
                        item.Role = Convert.ToString(record["Role"]);
                        break;
                    case "Email":
                        item.Email = Convert.ToString(record["Email"]);
                        break;
                    case "Contact1":
                        item.Contact1 = Convert.ToString(record["Contact1"]);
                        break;
                    case "Contact2":
                        item.Contact2 = Convert.ToString(record["Contact2"]);
                        break;
                }
            }
            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalDiscussion ToPortalDiscussion(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalDiscussion();

            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "Title":
                        item.Title = Convert.ToString(record["Title"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "Body":
                        item.Body = Convert.ToString(record["Body"]);
                        break;
                    case "DisplayOrder":
                        item.DisplayOrder = Convert.ToString(record["DisplayOrder"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "NextMessageID":
                        item.NextMessageId = record["NextMessageID"] == DBNull.Value
                                                 ? (int?)null
                                                 : Convert.ToInt32(record["NextMessageID"]);
                        break;
                    case "PrevMessageID":
                        item.PrevMessageId = record["PrevMessageID"] == DBNull.Value
                                                 ? (int?)null
                                                 : Convert.ToInt32(record["PrevMessageID"]);
                        break;
                    case "Parent":
                        item.Parent = Convert.ToString(record["Parent"]);
                        break;
                    case "ChildCount":
                        item.ChildCount = Convert.ToInt32(record["ChildCount"]);
                        break;
                    case "Indent":
                        item.Indent = Convert.ToInt32(record["Indent"]);
                        break;
                }
            }
            return item;
        }

        public static PortalDocument ToPortalDocument(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalDocument();

            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "FileNameUrl":
                        item.FileNameUrl = Convert.ToString(record["FileNameUrl"]);
                        break;
                    case "FileFriendlyName":
                        item.FileFriendlyName = Convert.ToString(record["FileFriendlyName"]);
                        break;
                    case "Category":
                        item.Category = Convert.ToString(record["Category"]);
                        break;
                    case "Content":
                        item.Content = record["Content"] as Byte[];
                        break;
                    case "ContentType":
                        item.ContentType = Convert.ToString(record["ContentType"]);
                        break;
                    case "ContentSize":
                        item.ContentSize = record["ContentSize"] == DBNull.Value
                                               ? 0
                                               : Convert.ToInt32(record["ContentSize"]);
                        break;
                }
            }
            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalEvent ToPortalEvent(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalEvent();
            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "Title":
                        item.Title = Convert.ToString(record["Title"]);
                        break;
                    case "WhereWhen":
                        item.WhereWhen = Convert.ToString(record["WhereWhen"]);
                        break;
                    case "Description":
                        item.Description = Convert.ToString(record["Description"]);
                        break;
                    case "ExpireDate":
                        item.ExpireDate = Convert.ToDateTime(record["ExpireDate"]);
                        break;
                }
            }
            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalHtmlText ToPortalHtmlText(this IDataRecord record)
        {
            var item = new PortalHtmlText();

            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "DesktopHtml":
                        item.DesktopHtml = Convert.ToString(record["DesktopHtml"]);
                        break;
                    case "MobileSummary":
                        item.MobileSummary = Convert.ToString(record["MobileSummary"]);
                        break;
                    case "MobileDetails":
                        item.MobileDetails = Convert.ToString(record["MobileDetails"]);
                        break;
                }
            }
            return item;
        }

        public static PortalLink ToPortalLink(this IDataRecord record, int itemId = -10)
        {
            var item = new PortalLink();
            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "ItemID":
                        item.ItemId = Convert.ToInt32(record["ItemID"]);
                        break;
                    case "ModuleID":
                        item.ModuleId = Convert.ToInt32(record["ModuleID"]);
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = Convert.ToString(record["CreatedByUser"]);
                        break;
                    case "CreatedDate":
                        item.CreatedDate = Convert.ToDateTime(record["CreatedDate"]);
                        break;
                    case "Title":
                        item.Title = Convert.ToString(record["Title"]);
                        break;
                    case "Url":
                        item.Url = Convert.ToString(record["Url"]);
                        break;
                    case "MobileUrl":
                        item.MobileUrl = Convert.ToString(record["MobileUrl"]);
                        break;
                    case "ViewOrder":
                        item.ViewOrder = Convert.ToInt32(record["ViewOrder"]);
                        break;
                    case "Description":
                        item.Description = Convert.ToString(record["Description"]);
                        break;
                }
            }
            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }
    }
}