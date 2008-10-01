using System;
using System.Configuration;
using System.IO;

namespace NW247.Infrastructure
{
    public static class ConfigManager
    {
        private static Configuration GetConfiguration(string configFileName)
        {
            var fileMap = new ExeConfigurationFileMap
                              {
                                  ExeConfigFilename = (configFileName)
                              };
            if (!File.Exists(fileMap.ExeConfigFilename))
            {
                throw new ApplicationException(string.Format("{0} file doesn't exist", configFileName));
            }
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        public static T GetConfigSettings<T>(string configFileName, string sectionName)
            where T : ConfigurationSection
        {
            Configuration config = GetConfiguration(configFileName);
            return (T) config.GetSection(sectionName);
        }
    }
}