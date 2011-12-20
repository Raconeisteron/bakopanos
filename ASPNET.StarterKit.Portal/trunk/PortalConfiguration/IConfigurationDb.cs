using System;
using System.Collections;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IConfigurationDb
    {
        /// <summary>
        /// The UpdatePortalInfo method updates the name and access settings for the portal.
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow);

        /// <summary>
        /// The AddTab method adds a new tab to the portal.  These settings are 
        /// stored in the Xml file PortalCfg.xml.
        /// </summary>        
        int AddTab(int portalId, String tabName, int tabOrder);

        /// <summary>
        /// The UpdateTab method updates the settings for the specified tab. 
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles,
                       String mobileTabName, bool showMobile);

        /// <summary>
        /// The UpdateTabOrder method changes the position of the tab with respect
        /// to other tabs in the portal.  These settings are stored in the Xml 
        /// file PortalCfg.xml.
        /// </summary>        
        void UpdateTabOrder(int tabId, int tabOrder);

        /// <summary>
        /// The DeleteTab method deletes the selected tab and its modules from 
        /// the settings which are stored in the Xml file PortalCfg.xml.  This 
        /// method also deletes any data from the database associated with all 
        /// modules within this tab.
        /// </summary>
        void DeleteTab(int tabId);

        /// <summary>
        /// The UpdateModuleOrder method updates the order in which the modules
        /// in a tab are displayed.  These settings are stored in the Xml file 
        /// PortalCfg.xml.
        /// </summary>        
        void UpdateModuleOrder(int moduleId, int moduleOrder, String pane);

        /// <summary>
        /// The AddModule method adds Portal Settings for a new Module within
        /// a Tab.  These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        int AddModule(int tabId, int moduleOrder, String paneName, String title, int moduleDefId,
                      int cacheTime,
                      String editRoles, bool showMobile);

        /// <summary>
        /// The UpdateModule method updates the Portal Settings for an existing 
        /// Module within a Tab.  These settings are stored in the Xml file
        /// PortalCfg.xml.
        /// </summary>
        int UpdateModule(int moduleId, int moduleOrder, String paneName, String title, int cacheTime,
                         String editRoles, bool showMobile);

        /// <summary>
        /// The DeleteModule method deletes a specified Module from the settings
        /// stored in the Xml file PortalCfg.xml.  This method also deletes any 
        /// data from the database associated with this module.
        /// </summary>        
        void DeleteModule(int moduleId);

        /// <summary>
        /// The UpdateModuleSetting Method updates a single module setting 
        /// in the configuration file.  If the value passed in is String.Empty,
        /// the Setting element is deleted if it exists.  If not, either a 
        /// matching Setting element is updated, or a new Setting element is 
        /// created.
        /// </summary>
        void UpdateModuleSetting(int moduleId, String key, String val);

        /// <summary>
        /// The GetModuleSettings Method returns a hashtable of custom,
        /// module-specific settings from the configuration file.  This method is
        /// used by some user control modules (Xml, Image, etc) to access misc
        /// settings.
        /// </summary>        
        Hashtable GetModuleSettings(int moduleId);

        /// <summary>
        /// The GetModuleDefinitions method returns a list of all module type 
        /// definitions for the portal.
        /// </summary>        
        DataRow[] GetModuleDefinitions(int portalId);

        /// <summary>
        /// The AddModuleDefinition add the definition for a new module type
        /// to the portal.
        /// </summary>        
        int AddModuleDefinition(int portalId, String name, String desktopSrc, String mobileSrc);

        /// <summary>
        /// The DeleteModuleDefinition method deletes the specified module type 
        /// definition from the portal.  Each module which is related to the
        /// ModuleDefinition is deleted from each tab in the configuration
        /// file, and all data relating to each module is deleted from the
        /// database.
        /// </summary>        
        void DeleteModuleDefinition(int defId);

        /// <summary>
        /// The UpdateModuleDefinition method updates the settings for the 
        /// specified module type definition.
        /// </summary>
        void UpdateModuleDefinition(int defId, String name, String desktopSrc, String mobileSrc);

        /// <summary>
        /// The Configuration.GetSiteSettings Method returns a typed
        /// dataset of the all of the site configuration settings from the
        /// XML configuration file.  This method is used in Global.asax to
        /// push the settings into the current HttpContext, so that all of the 
        /// pages, content modules and classes throughout the rest of the request
        /// may access them.
        ///
        /// The SiteConfiguration object is cached using the ASP.NET Cache API,
        /// with a file-change dependency on the XML configuration file.  Normallly,
        /// this method just returns a copy of the object in the cache.  When the
        /// configuration is updated and changes are saved to the the XML file,
        /// the SiteConfiguration object is evicted from the cache.  The next time 
        /// this method runs, it will read from the XML file again and insert a
        /// fresh copy of the SiteConfiguration into the cache.
        /// </summary>
        SiteConfiguration GetSiteSettings();

        /// <summary>
        /// The Configuration.SaveSiteSettings overwrites the the XML file with the
        /// settings in the SiteConfiguration object in context.  The object will in 
        /// turn be evicted from the cache and be reloaded from the XML file the next
        /// time GetSiteSettings() is called.
        /// </summary>
        void SaveSiteSettings();
    }
}