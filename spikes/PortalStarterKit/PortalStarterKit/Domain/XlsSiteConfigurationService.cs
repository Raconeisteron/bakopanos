using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalStarterKit.Domain
{
    public class XlsSiteConfigurationService : SiteConfigurationService
    {
        private readonly string _xlsFile;
        readonly List<PortalEntity> _portalList;
        readonly List<TabEntity> _tabList;
        readonly List<ModuleEntity> _moduleList;
        readonly List<TabDefinitionEntity> _tabDefList;
        readonly List<ModuleDefinitionEntity> _moduleDefList;

        public XlsSiteConfigurationService(string xlsFile)
        {
            _xlsFile = xlsFile;                

            _portalList = GetList<PortalEntity>("Portals", ToPortal);
            _tabList = GetList<TabEntity>("Tabs", ToTab);
            _moduleList = GetList<ModuleEntity>("Modules", ToModule);
            _tabDefList = GetList<TabDefinitionEntity>("TabDefinitions", ToTabDefinition);
            _moduleDefList = GetList<ModuleDefinitionEntity>("ModuleDefinitions", ToModuleDefinition);

            SiteConfiguration = new SiteConfiguration();

            SiteConfiguration.TabDefinitions = new List<TabDefinition>();
            foreach (TabDefinitionEntity entity in _tabDefList)
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
            foreach (ModuleDefinitionEntity entity in _moduleDefList)
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
            foreach (PortalEntity portalEntity in _portalList)
            {
                var portal = new Portal
                                 {
                                     PortalId = portalEntity.PortalId,
                                     PortalName = portalEntity.PortalName,
                                     AlwaysShowEditButton = portalEntity.AlwaysShowEditButton,
                                     Tabs = new List<Tab>()
                                 };

                SiteConfiguration.Portals.Add(portal);
                MakeTabs(portal.Tabs,portal.PortalId,0);
            }

            foreach (Portal portal in SiteConfiguration.Portals)
            {
                InitializeSiteConfiguration(portal.Tabs);
            }
        }

        private void MakeTabs(List<Tab> tabs, int portalId, int parentTabId)
        {
            foreach (TabEntity tabEntity in _tabList.Where(item => item.PortalId == portalId).Where(item => item.ParentTabId == parentTabId))
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
                tabs.Add(tab);
                foreach (ModuleEntity moduleEntity in _moduleList.Where(item => item.TabId == tabEntity.TabId))
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
                MakeTabs(tab.Tabs, portalId, tab.TabId);
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
                ParentTabId = Convert.ToInt32(items["ParentTabId"]),
                TabDefId = Convert.ToInt32(items["TabDefId"]),
                TabName = items["TabName"] as string
            };
            return item;
        }

        private class TabEntity
        {
            public int PortalId { get; set; }
            public int TabId { get; set; }
            public int ParentTabId { get; set; }
            public int TabDefId { get; set; }
            public string TabName { get; set; }
            public int TabOrder { get; set; }
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
            if (File.Exists(_xlsFile))
            {
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _xlsFile +
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