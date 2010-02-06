using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Extensions on the .net System.Configuration.ConfigurationManager
    /// </summary>
    public static class ConfigManager
    {       
        /// <summary>
        /// Gets the configuration section.
        /// </summary>        
        /// <param name="configFileName">Name of the config file.</param>
        /// <param name="sectionName">Name of the section.</param>        
        public static T GetSection<T>(string configFileName, string sectionName)            
            where T : class 
        {
            Configuration config = GetConfiguration(configFileName);
            return config.GetSection(sectionName) as T;            
        }

        /// <summary>
        /// Gets the sections.
        /// </summary>        
        public static List<T> GetSections<T>(string configFile)
            where T : class
        {
            Configuration config = GetConfiguration(configFile);
            ConfigurationSectionCollection sectionCollection = config.Sections;

            var sections = new List<T>(sectionCollection.Count);

            foreach (var section in sectionCollection)
            {
                if (section is T)
                {
                    sections.Add(section as T);
                }
            }

            return sections;
        }

        /// <summary>
        /// Get the Configuration per file....
        /// </summary>
        /// <param name="configFileName">Name of the config file.</param>
        private static Configuration GetConfiguration(string configFileName)            
        {
            if (!File.Exists(configFileName))
            {
                throw new FileNotFoundException("Configuration file missing", configFileName);
            }
            var fileMap = new ConfigurationFileMap
                              {
                                  MachineConfigFilename = (configFileName)
                              };
            Configuration config = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            return config;            
        }

    }
}