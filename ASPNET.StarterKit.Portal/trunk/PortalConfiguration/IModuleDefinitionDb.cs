using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IModuleDefinitionDb
    {
        ModuleDefinitionItem GetModuleDefinitionByModuleDefId(int moduleDefId);

        /// <summary>
        /// The GetModuleDefinitions method returns a list of all module type 
        /// definitions for the portal.
        /// </summary>        
        IEnumerable<ModuleDefinitionItem> GetModuleDefinitions(int portalId);

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
        /// The GetSingleModuleDefinition method returns a ModuleDefinitionRow
        /// object containing details about a specific module definition in the
        /// configuration file.
        /// </summary>        
        ModuleDefinitionItem GetSingleModuleDefinition(int defId);
    }
}