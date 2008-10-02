using System;
using System.Configuration;
using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NW247.Services;

namespace NW247
{
    internal class TestHelper
    {
        public static IUnityContainer IntegrationTestContainer()
        {
            var container = new UnityContainer();
            var config =
                GetConfigSettings<UnityConfigurationSection>("NW247.Shell.exe.config", "unity");
            config.Containers.Default.Configure(container);
            return container;
        }

        public static IProductsService GetProductsService()
        {
            var container = new UnityContainer();
            container.RegisterType<IProductsService, ProductsService>();
            var service = container.Resolve<IProductsService>();
            return service;
        }

        #region config manager

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

        private static T GetConfigSettings<T>(string configFileName, string sectionName)
            where T : ConfigurationSection
        {
            Configuration config = GetConfiguration(configFileName);
            return (T) config.GetSection(sectionName);
        }

        #endregion
    }
}