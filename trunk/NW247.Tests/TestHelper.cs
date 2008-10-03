using System;
using System.Configuration;
using System.IO;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NW247.Data.NorthwindDataSetTableAdapters;
using NW247.Model;
using NW247.Module.Views;
using NW247.Services;
using Rhino.Mocks;

namespace NW247
{
    public class TestHelper
    {
        public IUnityContainer IntegrationTestContainer()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            var config =
                GetConfigSettings<UnityConfigurationSection>("NW247.Shell.exe.config", "unity");
            config.Containers.Default.Configure(container);
            
            return container;
        }

        public IUnityContainer UnitTestContainer()
        {
            var mock = new MockRepository();
            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterInstance<ProductsTableAdapter>(mock.Stub<ProductsTableAdapter>());
            return container;
        }

        public IProductsService GetProductsService()
        {
            IUnityContainer container = UnitTestContainer();
            container.RegisterType<IProductsService, ProductsService>();
            var service = container.Resolve<IProductsService>();

            return service;
        }

        public IProductsPresenter GetProductsPresenter()
        {
            IUnityContainer container = UnitTestContainer();           
            container.RegisterType<IProductsService, ProductsService>();
            var service = container.Resolve<IProductsService>();

            return new ProductsPresenter(service, container);
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