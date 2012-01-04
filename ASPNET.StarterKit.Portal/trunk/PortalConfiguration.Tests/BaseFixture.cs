using System;
using System.IO;
using System.Web;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;

namespace ASPNETPortal.Configuration
{
    public class BaseFixture<T>
    {
        protected T Db { get; private set; }

        [SetUp]
        public void SetUp()
        {
            string configFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            File.Copy("PortalCfg.xml", configFile);

            var mocks = new MockRepository();

            //container initialization 
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance("ConfigFile", configFile);
            container.RegisterInstance(mocks.DynamicMock<IPortalModulesDb>());
            container.RegisterInstance(mocks.Stub<HttpContextBase>());

            Type type = Type.GetType("ASPNETPortal.ConfigurationDb,PortalConfiguration");
            Db = (T) container.Resolve(type);
        }
    }
}