using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace DeadDevsSociety.UnityIntro
{
    class Program
    {
        static void Main(string[] args)
        {            
            string containerConfiguratorConfigFile =
                ConfigurationManager.AppSettings["containerConfiguratorConfigFile"] as string;

            ConfigurationSectionCollection configSections = ConfigSections(containerConfiguratorConfigFile);
            
            Func<string, IContainerConfigurator> fSection = 
                (name) => configSections[name] as IContainerConfigurator;

            new UnityContainer().
                ConfigureSection(fSection("core")).
                ConfigureSection<IDataConfig>(fSection("data")).
                ConfigureSection<IFacadeConfig>(fSection("facade")).
                ConfigureSection<IPresentationConfig>(fSection("presentation")).
                Resolve<IView>();
         
            Console.ReadLine();
        }

        static ConfigurationSectionCollection ConfigSections(string configFile)
        {
            var fileMap = new ConfigurationFileMap()
            {
                MachineConfigFilename = (configFile)
            };
            Configuration config = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            return config.Sections;
        }
    }

    public static class ContainerConfigurator 
    {       
        public static IUnityContainer ConfigureSection(this IUnityContainer container, IContainerConfigurator containerConfigurator)
        {
            return containerConfigurator.Configure(container);
        }

        public static IUnityContainer ConfigureSection<T>(this IUnityContainer container, IContainerConfigurator containerConfigurator)
            where T:class
        {
            return containerConfigurator.Configure(container).RegisterInstance<T>(containerConfigurator as T);
        }
    }

    public interface IContainerConfigurator 
    {
        IUnityContainer Configure(IUnityContainer container);
    }

    public class PresentationContainerConfigurator : ConfigurationSection, IContainerConfigurator, IPresentationConfig
    {        
        public IUnityContainer Configure(IUnityContainer container)
        {            
            container.RegisterType<IView, View>(new ContainerControlledLifetimeManager());            
            return container;
        }        
    }

    public interface IPresentationConfig
    {

    }

    public class FacadeContainerConfigurator : ConfigurationSection, IContainerConfigurator, IFacadeConfig
    {
        public IUnityContainer Configure(IUnityContainer container)
        {            
            container.RegisterType<IFacade, Facade>(new ContainerControlledLifetimeManager());
            return container;
        }
    }

    public interface IFacadeConfig
    {
        
    }

    public class DataContainerConfigurator : ConfigurationSection, IContainerConfigurator, IDataConfig
    {
        public IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterType<IData, Data>(new ContainerControlledLifetimeManager());
            return container;
        }

        [ConfigurationProperty("connString")]
        public string ConnString
        {
            get { return this["connString"] as string; }            
        }
    }

    public interface IDataConfig
    {        
        string ConnString
        {
            get;            
        }
    }

    public class CoreContainerConfigurator : ConfigurationSection, IContainerConfigurator, ICoreConfig
    {
        public IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterInstance<ICoreConfig>(this);
            container.RegisterType<ILog, Log>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<LogSeverity>((LogSeverity)Enum.Parse(typeof(LogSeverity), Severity, false));

            return container;
        }

        [ConfigurationProperty("severity")]
        public string Severity
        {
            get
            {                
                return this["severity"] as string;
            }
        }
    }

    public interface ICoreConfig
    {
        string Severity
        {
            get;
        }
    }

    public class View : IView
    {
        public View(IFacade facade, ILog log)
        {
            log.Info("c'tor", "View");
        }
    }

    public interface IView
    {
    }

    public class Facade : IFacade
    {
        public Facade(IData data, ILog log)
        {
            log.Info("c'tor", "Facade");
        }
    }

    public interface IFacade
    {

    }

    public class Data : IData
    {
        public Data(ILog log, IDataConfig config)
        {
            log.Info("c'tor", "Data");
        }

    }

    public interface IData
    {
        
    }

    public class Log : ILog
    {
        LogSeverity _severity;        

        public Log(LogSeverity severity)
        {
            _severity = severity;
            IsOff = _severity == LogSeverity.Off;
            IsVerbose = _severity <= LogSeverity.Verbose;
            IsDebug = _severity <= LogSeverity.Debug;
            IsInfo = _severity <= LogSeverity.Info;
            IsError = _severity <= LogSeverity.Error;            
        }

        public Action<string, string> actionTrace =
            (category, message) => Trace.WriteLine(string.Format("{0}: {1}", category, message));

        public bool IsOff { get; private set; }
        public bool IsVerbose{get;private set;}
        public bool IsDebug{get;private set;}
        public bool IsInfo{get;private set;}
        public bool IsError{get;private set;}

        public ILog Verbose(string message, string category)
        {
            if (IsVerbose)
            {
                actionTrace(category, message);
            }
            return this;
        }
        public ILog Debug(string message, string category)
        {
            if (IsDebug)
            {
                actionTrace(category, message);
            }
            return this;
        }
        public ILog Info(string message, string category)
        {
            if (IsInfo)
            {
                actionTrace(category, message);
            }
            return this;
        }
        public ILog Error(string message, string category)
        {
            if (IsError)
            {
                actionTrace(category, message);
            }
            return this;
        }
    }

    public interface ILog
    {
        bool IsOff { get; }
        bool IsVerbose { get; }
        bool IsDebug { get; }
        bool IsInfo { get; }
        bool IsError { get; }

        ILog Verbose(string message, string category);
        ILog Debug(string message, string category);
        ILog Info(string message, string category);
        ILog Error(string message, string category);
    }

    public enum LogSeverity
    {
        Off, Verbose, Debug, Info, Error
    }

}
