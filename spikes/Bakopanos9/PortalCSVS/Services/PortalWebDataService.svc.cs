using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel;

//only a sample....
//http://msdn.microsoft.com/en-us/library/cc907912.aspx

namespace ASPNET.StarterKit.Portal
{
    [DataServiceKey("ItemId")]
    public partial class PortalAnnouncement
    {
    }

    public partial class PortalAnnouncement
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MoreLink { get; set; }
        public string MobileMoreLink { get; set; }
    }

    [DataServiceKey("ItemId")]
    public partial class PortalContact
    {
    }

    public partial class PortalContact
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
    }

    [DataServiceKey("ItemId")]
    public partial class PortalEvent
    {
    }

    public partial class PortalEvent
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WhereWhen { get; set; }
    }

    [DataServiceKey("ItemId")]
    public partial class PortalLink
    {
    }

    public partial class PortalLink
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string MobileUrl { get; set; }
        public int? ViewOrder { get; set; }
    }

    public class PortalContext
    {
        /*
        public IQueryable<PortalAnnouncement> Announcements
        {
            get
            {
                DataSet dataSet = DataAccess.AnnouncementsDB.GetAnnouncements();
                var list = new List<PortalAnnouncement>();

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var item = new PortalAnnouncement
                                   {
                                       ModuleId = Convert.ToInt32(row["ModuleId"]),
                                       ItemId = Convert.ToInt32(row["ItemId"]),
                                       CreatedByUser = row["CreatedByUser"] as string,
                                       Title = row["Title"] as string,
                                       Description = row["Description"] as string,
                                       ExpireDate = row["ExpireDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ExpireDate"]),
                                       CreatedDate = row["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CreatedDate"]),
                                       MobileMoreLink = row["MobileMoreLink"] as string,
                                       MoreLink = row["MoreLink"] as string
                                   };

                    list.Add(item);
                }

                return list.AsQueryable();
            }
        }

        public IQueryable<PortalContact> Contacts
        {
            get
            {
                DataSet dataSet = DataAccess.ContactsDB.GetContacts();
                var list = new List<PortalContact>();

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var item = new PortalContact
                                   {
                                       ModuleId = Convert.ToInt32(row["ModuleId"]),
                                       ItemId = Convert.ToInt32(row["ItemId"]),
                                       CreatedByUser = row["CreatedByUser"] as string,
                                       CreatedDate = row["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CreatedDate"]),
                                       Email = row["Email"] as string,
                                       Name = row["Name"] as string,
                                       Role = row["Role"] as string,
                                       Contact1 = row["Contact1"] as string,
                                       Contact2 = row["Contact2"] as string
                                   };

                    list.Add(item);
                }

                return list.AsQueryable();
            }
        }

        public IQueryable<PortalEvent> Events
        {
            get
            {
                DataSet dataSet = DataAccess.EventsDB.GetEvents();
                var list = new List<PortalEvent>();

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var item = new PortalEvent
                                   {
                                       ModuleId = Convert.ToInt32(row["ModuleId"]),
                                       ItemId = Convert.ToInt32(row["ItemId"]),
                                       CreatedByUser = row["CreatedByUser"] as string,
                                       Title = row["Title"] as string,
                                       Description = row["Description"] as string,
                                       ExpireDate = row["ExpireDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ExpireDate"]),
                                       CreatedDate = row["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CreatedDate"]),
                                       WhereWhen = row["WhereWhen"] as string
                                   };

                    list.Add(item);
                }

                return list.AsQueryable();
            }
        }

        public IQueryable<PortalLink> Links
        {
            get
            {
                IDataReader reader = DataAccess.LinksDB.GetLinks();
                var list = new List<PortalLink>();

                while (reader.Read())
                {
                    var item = new PortalLink
                                   {
                                       ModuleId = Convert.ToInt32(reader["ModuleId"]),
                                       ItemId = Convert.ToInt32(reader["ItemId"]),
                                       CreatedByUser = reader["CreatedByUser"] as string,
                                       Title = reader["Title"] as string,
                                       Description = reader["Description"] as string,
                                       CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CreatedDate"]),
                                       MobileUrl = reader["MobileUrl"] as string,
                                       Url = reader["Url"] as string,
                                       ViewOrder = reader["ViewOrder"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ViewOrder"])
                                   };

                    list.Add(item);
                }

                return list.AsQueryable();
            }
        }
        */
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PortalWebDataService : DataService<PortalContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(IDataServiceConfiguration config)
        {
            // ### tell us as much as you can           
            config.UseVerboseErrors = true;
            // specify permissions for entity-sets and operations    
            // debug only; don't do this!            
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            //config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);          
        }
    }
}