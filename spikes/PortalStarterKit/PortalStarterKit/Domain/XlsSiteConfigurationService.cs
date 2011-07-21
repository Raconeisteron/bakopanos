using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalStarterKit.Domain
{
    [Export]
    public class XlsSiteConfigurationService : SiteConfigurationService
    {
        private readonly string _xlsSiteConfigurationFile;

        public XlsSiteConfigurationService()
        {
            _xlsSiteConfigurationFile =
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XlsSiteConfigurationFile"]);

            var portalList = GetList<PortalEntity>("Portals", ToPortal);
            var tabList = GetList<TabEntity>("Tabs", ToTab);
            var moduleList = GetList<ModuleEntity>("Modules", ToModule);
            var tabDefList = GetList<TabDefinitionEntity>("TabDefinitions", ToTabDefinition);
            var moduleDefList = GetList<ModuleDefinitionEntity>("ModuleDefinitions", ToModuleDefinition);

            SiteConfiguration = new SiteConfiguration();

            SiteConfiguration.TabDefinitions = new List<TabDefinition>();
            foreach (TabDefinitionEntity entity in tabDefList)
            {
                var item = new TabDefinition
                               {
                                   TabDefId = entity.TabDefId,
                                   FriendlyName = entity.FriendlyName,
                                   SourceFile = entity.SourceFile
                               };
                SiteConfiguration.TabDefinitions.Add(item);
            }

            SiteConfiguration.ModuleDefinitions = new List<ModuleDefinition>();
            foreach (ModuleDefinitionEntity entity in moduleDefList)
            {
                var item = new ModuleDefinition
                               {
                                   ModuleDefId = entity.ModuleDefId,
                                   FriendlyName = entity.FriendlyName,
                                   SourceFile = entity.SourceFile
                               };
                SiteConfiguration.ModuleDefinitions.Add(item);
            }

            SiteConfiguration.Portals = new List<Portal>();
            foreach (PortalEntity portalEntity in portalList)
            {
                var portal = new Portal
                                 {
                                     PortalId = portalEntity.PortalId,
                                     PortalName = portalEntity.PortalName,
                                     AlwaysShowEditButton = portalEntity.AlwaysShowEditButton,
                                     Tabs = new List<Tab>()
                                 };

                SiteConfiguration.Portals.Add(portal);
                foreach (TabEntity tabEntity in tabList.Where(item => item.PortalId == portalEntity.PortalId))
                {
                    var tab = new Tab
                                  {
                                      TabId = tabEntity.TabId,
                                      TabName = tabEntity.TabName,
                                      Modules = new List<Module>(),
                                      TabDefId = tabEntity.TabDefId,
                                      TabOrder = tabEntity.TabOrder,
                                      Tabs = new List<Tab>()
                                  };
                    portal.Tabs.Add(tab);
                    foreach (ModuleEntity moduleEntity in moduleList.Where(item => item.TabId == tabEntity.TabId))
                    {
                        var module = new Module
                                         {
                                             ModuleId = moduleEntity.ModuleId,
                                             ModuleDefId = moduleEntity.ModuleDefId,
                                             ModuleTitle = moduleEntity.ModuleTitle,
                                             ModuleOrder = moduleEntity.ModuleOrder,
                                             PaneName = moduleEntity.PaneName
                                         };
                        tab.Modules.Add(module);
                    }
                }
            }

            foreach (Portal portal in SiteConfiguration.Portals)
            {
                InitializeSiteConfiguration(portal.Tabs);
            }
        }

        private static PortalEntity ToPortal(IDataRecord items)
        {
            var item = new PortalEntity
                           {
                               PortalId = Convert.ToInt32(items["PortalId"]),
                               PortalName = items["PortalName"] as string,
                               AlwaysShowEditButton = Convert.ToBoolean(items["AlwaysShowEditButton"])
                           };
            return item;
        }

        private class PortalEntity
        {
            public int PortalId { get; set; }            
            public string PortalName { get; set; }            
            public bool AlwaysShowEditButton { get; set; }            
        }

        private static TabEntity ToTab(IDataRecord items)
        {
            var item = new TabEntity
            {
                PortalId = Convert.ToInt32(items["PortalId"]),
                TabId = Convert.ToInt32(items["TabId"]),
                TabDefId = Convert.ToInt32(items["TabDefId"]),
                TabName = items["TabName"] as string
            };
            return item;
        }

        private class TabEntity
        {
            public int PortalId { get; set; }
            public int TabId { get; set; }
            public int TabDefId { get; set; }
            public string TabName { get; set; }
            public int TabOrder { get; set; }
            public string NavigateUrl { get; set; }            
        }

        private static ModuleEntity ToModule(IDataRecord items)
        {
            var item = new ModuleEntity
            {
                ModuleId = Convert.ToInt32(items["ModuleId"]),
                ModuleTitle = items["ModuleTitle"] as string
            };
            return item;
        }

        private class ModuleEntity
        {
            public int TabId { get; set; }
            public int ModuleId { get; set; }
            public int ModuleDefId { get; set; }
            public int ModuleOrder { get; set; }
            public string ModuleTitle { get; set; }
            public PaneType PaneName { get; set; }            
        }

        private static TabDefinitionEntity ToTabDefinition(IDataRecord items)
        {
            var item = new TabDefinitionEntity
            {
                TabDefId = Convert.ToInt32(items["TabDefId"]),
                FriendlyName = items["FriendlyName"] as string,
                SourceFile = items["DesktopSourceFile"] as string
            };
            return item;
        }

        private class TabDefinitionEntity
        {
            public int TabDefId { get; set; }
            public string FriendlyName { get; set; }
            public string SourceFile { get; set; }
        }

        private static ModuleDefinitionEntity ToModuleDefinition(IDataRecord items)
        {
            var item = new ModuleDefinitionEntity
            {
                ModuleDefId = Convert.ToInt32(items["ModuleDefId"]),
                FriendlyName = items["FriendlyName"] as string,
                SourceFile = items["DesktopSourceFile"] as string
            };
            return item;
        }

        private class ModuleDefinitionEntity
        {
            public int ModuleDefId { get; set; }
            public string FriendlyName { get; set; }
            public string SourceFile { get; set; }
        }

        private List<T> GetList<T>(string settingsTableName, Func<OleDbDataReader, T> toT)
        {
            var list = new List<T>();
            if (File.Exists(_xlsSiteConfigurationFile))
            {
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _xlsSiteConfigurationFile +
                                          @";Extended Properties=""Excel 8.0;HDR=YES;""";

                var connection = new OleDbConnection(connectionString);
                connection.Open();

                string commandText = "Select * From [" + settingsTableName + "$] ";

                var command = new OleDbCommand(commandText, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(toT(reader));
                }

                connection.Close();
            }

            return list;
        }



    }
}