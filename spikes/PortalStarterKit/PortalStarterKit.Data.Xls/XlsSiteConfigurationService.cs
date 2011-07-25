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
    public class XlsSiteConfigurationService : SiteConfigurationService
    {
        private string _xlsFile;
        private List<PortalEntity> _portalList;
        private List<TabEntity> _tabList;
        private List<ModuleEntity> _moduleList;
        private List<TabDefinitionEntity> _tabDefList;
        private List<ModuleDefinitionEntity> _moduleDefList;

        public override SiteConfiguration ReadSiteConfiguration()
        {
            _xlsFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XlsSiteConfigurationFile"]);

            _portalList = GetList<PortalEntity>("Portals", item => new PortalEntity(item));
            _tabList = GetList<TabEntity>("Tabs", item => new TabEntity(item));
            _moduleList = GetList<ModuleEntity>("Modules", item => new ModuleEntity(item));
            _tabDefList = GetList<TabDefinitionEntity>("TabDefinitions", item => new TabDefinitionEntity(item));
            _moduleDefList = GetList<ModuleDefinitionEntity>("ModuleDefinitions", item => new ModuleDefinitionEntity(item));

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
                MakeTabs(tab, portalId, tab.TabId);
            }
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