using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace FunqWithUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            
            #region example1
            /*
            container.RegisterType<IPresentation, Presentation>();
            container.RegisterType<IApplicationServices, ApplicationServices>();
            container.RegisterType<IDataServices, DataServices>();

            var presentation = container.Resolve<IPresentation>();

            presentation.Show();
            */
            #endregion

            /*
            container.RegisterInstance<string>(ConnectionType.Ora, "#connstring#ora#", 
                new ContainerControlledLifetimeManager());
            container.RegisterInstance<string>(ConnectionType.Sql, "#connstring#sql#", 
                new ContainerControlledLifetimeManager());
            container.RegisterInstance<string>(ConnectionType.MsAccess, "#connstring#access#", 
                new ContainerControlledLifetimeManager());

            container.Resolve<DataService>();
            */

            Func<ConnectionType, string> fConnString = delegate (ConnectionType connectionType)
            {
                switch (connectionType)
                {
                    case ConnectionType.Ora:
                        return "#connstring#ora#";
                    case ConnectionType.Sql:
                        return "#connstring#sql#";
                    case ConnectionType.MsAccess:
                        return "#connstring#access#";
                    default:
                        return "#connstring#ora#";
                }
            };

            container.RegisterInstance<Func<ConnectionType, string>>(fConnString,
                new ContainerControlledLifetimeManager());

            container.Resolve<DataService>();

            Console.ReadLine();
        }
    }

    /*
    public static class ConnectionType
    {
        public const string Ora = "Ora";
        public const string Sql = "Sql";
        public const string MsAccess = "MsAccess";
    }

    public class DataService
    {
        public DataService([Dependency(ConnectionType.Ora)]string connectionString)
        {
            Console.WriteLine( connectionString );
            //create connection here...
        }
    }
    */

    public enum ConnectionType
    {
        Ora, Sql, MsAccess
    }

    public class DataService
    {
        public DataService(Func<ConnectionType,string> connectionString)
        {
            Console.WriteLine(connectionString(ConnectionType.Ora));
            //create connection here...
        }
    }

    #region example1
    /*
    public interface IPresentation
    {
        void Show();
    }

    public class Presentation : IPresentation
    {
        IApplicationServices _appServices;

        public Presentation(IApplicationServices appServices)
        {
            Console.WriteLine("c'tor Presentation()");
            _appServices = appServices;
        }

        public void Show()
        {
            foreach(string item in _appServices.GetData("a"))
            {
                Console.WriteLine(item);
            }
        }

    }

    public interface IApplicationServices
    {
        List<string> GetData(string filter);
    }

    public class ApplicationServices : IApplicationServices
    {
        [Dependency]
        public IDataServices DataServices { private get; set; }

        public ApplicationServices()
        {
            Console.WriteLine("c'tor ApplicationServices()");
        }

        public List<string> GetData(string filter)
        {
            return (from item in DataServices.GetData()
                   where item.Contains(filter)
                   select item).ToList<string>();
        }

    }

    public interface IDataServices
    {
        List<string> GetData();
    }

    public class DataServices : IDataServices
    {
        public DataServices()
        {
            Console.WriteLine("c'tor DataServices()");
        }

        public List<string> GetData()
        {
            return new List<string> { "aa","a", "b","abc", "c" };
        }
    }    
    */
    #endregion
}
