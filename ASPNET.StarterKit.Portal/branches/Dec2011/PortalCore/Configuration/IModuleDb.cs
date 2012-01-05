using System;
using System.Collections;
using System.Collections.Generic;

namespace ASPNETPortal.Configuration
{
    public interface IModuleDb
    {
        IEnumerable<ModuleItem> GetModulesByTabId(int tabId);

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

        string GetEditRolesByModuleId(int moduleId);

        string GetAccessRolesByModuleId(int moduleId);
    }
}