using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data.Xls
{

    public class ComponentConfiguration : ConfigurationSection, IComponentConfiguration
    {
        [ConfigurationProperty("XlsFile")]
        public string XlsFile
        {
            get
            {
                return this["XlsFile"] as string;
            }
        }
   
        private List<PortalEntity> _portalList;
        private List<TabEntity> _tabList;
        private List<ModuleEntity> _moduleList;
        private List<TabDefinitionEntity> _tabDefList;
        private List<ModuleDefinitionEntity> _moduleDefList;

        public SiteConfiguration ReadSiteConfiguration(Func<string, string> serverMapPath)
        {
            string xlsFileName = serverMapPath(XlsFile);

            _portalList = GetList<PortalEntity>(xlsFileName, "Portals", item => new PortalEntity(item));
            _tabList = GetList<TabEntity>(xlsFileName, "Tabs", item => new TabEntity(item));
            _moduleList = GetList<ModuleEntity>(xlsFileName, "Modules", item => new ModuleEntity(item));
            _tabDefList = GetList<TabDefinitionEntity>(xlsFileName, "TabDefinitions", item => new TabDefinitionEntity(item));
            _moduleDefList = GetList<ModuleDefinitionEntity>(xlsFileName, "ModuleDefinitions", item => new ModuleDefinitionEntity(item));

            var configuration = new SiteConfiguration();

            foreach (TabDefinitionEntity entity in _tabDefList)
            {
                var item = new TabDefinition
                               {
                                   TabDefId = entity.TabDefId,
                                   FriendlyName = entity.FriendlyName,
                                   SourceFile = entity.SourceFile
                               };
                configuration.TabDefinitions.Add(item);
            }

            foreach (ModuleDefinitionEntity entity in _moduleDefList)
            {
                var item = new ModuleDefinition
                               {
                                   ModuleDefId = entity.ModuleDefId,
                                   FriendlyName = entity.FriendlyName,
                                   SourceFile = entity.SourceFile
                               };
                configuration.ModuleDefinitions.Add(item);
            }

            foreach (PortalEntity portalEntity in _portalList)
            {
                var portal = configuration.NewPortal();
                portal.PortalId = portalEntity.PortalId;
                portal.PortalName = portalEntity.PortalName;
                portal.AlwaysShowEditButton = portalEntity.AlwaysShowEditButton;                                 

                configuration.Portals.Add(portal);
                MakeTabs(portal,portal.PortalId,0);
            }

            return configuration;
        }

        private void MakeTabs(ITabContainer tabContainer, int portalId, int parentTabId)
        {
            foreach (TabEntity tabEntity in _tabList.Where(item => item.PortalId == portalId).Where(item => item.ParentTabId == parentTabId))
            {
                var tab = tabContainer.NewTab();
                tab.TabId = tabEntity.TabId;
                tab.TabName = tabEntity.TabName;
                tab.TabDefId = tabEntity.TabDefId;
                tab.TabOrder = tabEntity.TabOrder;
            
                tabContainer.Tabs.Add(tab);
                foreach (ModuleEntity moduleEntity in _moduleList.Where(item => item.TabId == tabEntity.TabId))
                {
                    var module = tab.NewModule();

                    module.ModuleId = moduleEntity.ModuleId;
                    module.ModuleDefId = moduleEntity.ModuleDefId;
                    module.ModuleTitle = moduleEntity.ModuleTitle;
                    module.ModuleOrder = moduleEntity.ModuleOrder;
                    module.PaneName = moduleEntity.PaneName;
                    
                    tab.Modules.Add(module);
                }
                MakeTabs(tab, portalId, tab.TabId);
            }
        }

        private static List<T> GetList<T>(string xlsFileName, string settingsTableName, Func<OleDbDataReader, T> toT)
        {
            var list = new List<T>();
            if (File.Exists(xlsFileName))
            {
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + xlsFileName +
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