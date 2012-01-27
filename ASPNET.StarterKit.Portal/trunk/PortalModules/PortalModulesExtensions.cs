using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    //All the switch cases used in conjuction with SPROC field names retrieval (record.GetName) are *Case Sensitive*.
    public static class PortalModulesExtensions
    {
        public static PortalAnnouncement ToPortalAnnouncement(this IDataRecord record, int itemId=-10)
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
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime)record["CreatedDate"];
                        break;
                    case "Title":
                        item.Title = record["Title"] as string;
                        break;
                    case "MoreLink":
                        item.MoreLink = record["MoreLink"] as string;
                        break;
                    case "MobileMoreLink":
                        item.MobileMoreLink = record["MobileMoreLink"] as string;
                        break;
                    case "ExpireDate":
                        item.ExpireDate = (DateTime)record["ExpireDate"];
                        break;
                    case "Description":
                        item.Description = record["Description"] as string;
                        break;
                }
            }

            //Explicit call with itemId provided
            if (itemId!=-10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalContact ToPortalContact(this IDataRecord record, int itemId=-10)
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
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime) record["CreatedDate"];
                        break;
                    case "Name":
                        item.Name = record["Name"] as string;
                        break;
                    case "Role":
                        item.Role = record["Role"] as string;
                        break;
                    case "Email":
                        item.Email = record["Email"] as string;
                        break;
                    case "Contact1":
                        item.Contact1 = record["Contact1"] as string;
                        break;
                    case "Contact2":
                        item.Contact2 = record["Contact2"] as string;
                        break;
                }
            }
            //Explicit call with itemId provided
            if (itemId != -10)
                item.ItemId = itemId;
            return item;
        }

        public static PortalDiscussion ToPortalDiscussion(this IDataRecord record, int itemId=-10)
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
                        item.Title = record["Title"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime) record["CreatedDate"];
                        break;
                    case "Body":
                        item.Body = record["Body"] as string;
                        break;
                    case "DisplayOrder":
                        item.DisplayOrder = record["DisplayOrder"] as string;
                        break;
                    case "CreatedByUser":
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "NextMessageID":
                        item.NextMessageId = record["NextMessageID"] == DBNull.Value
                                                 ? (int?) null
                                                 : Convert.ToInt32(record["NextMessageID"]);
                        break;
                    case "PrevMessageID":
                        item.PrevMessageId = record["PrevMessageID"] == DBNull.Value
                                                 ? (int?) null
                                                 : Convert.ToInt32(record["PrevMessageID"]);
                        break;
                    case "Parent":
                        item.Parent = record["Parent"] as string;
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
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime) record["CreatedDate"];
                        break;
                    case "FileNameUrl":
                        item.FileNameUrl = record["FileNameUrl"] as string;
                        break;
                    case "FileFriendlyName":
                        item.FileFriendlyName = record["FileFriendlyName"] as string;
                        break;
                    case "Category":
                        item.Category = record["Category"] as string;
                        break;
                    case "Content":
                        item.Content = record["Content"] as Byte[];
                        break;
                    case "ContentType":
                        item.ContentType = record["ContentType"] as string;
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
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime) record["CreatedDate"];
                        break;
                    case "Title":
                        item.Title = record["Title"] as string;
                        break;
                    case "WhereWhen":
                        item.WhereWhen = record["WhereWhen"] as string;
                        break;
                    case "Description":
                        item.Description = record["Description"] as string;
                        break;
                    case "ExpireDate":
                        item.ExpireDate = (DateTime) record["ExpireDate"];
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
                        item.DesktopHtml = record["DesktopHtml"] as string;
                        break;
                    case "MobileSummary":
                        item.MobileSummary = record["MobileSummary"] as string;
                        break;
                    case "MobileDetails":
                        item.MobileDetails = record["MobileDetails"] as string;
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
                        item.CreatedByUser = record["CreatedByUser"] as string;
                        break;
                    case "CreatedDate":
                        item.CreatedDate = (DateTime) record["CreatedDate"];
                        break;
                    case "Title":
                        item.Title = record["Title"] as string;
                        break;
                    case "Url":
                        item.Url = record["Url"] as string;
                        break;
                    case "MobileUrl":
                        item.MobileUrl = record["MobileUrl"] as string;
                        break;
                    case "ViewOrder":
                        item.ViewOrder = Convert.ToInt32(record["ViewOrder"]);
                        break;
                    case "Description":
                        item.Description = record["Description"] as string;
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