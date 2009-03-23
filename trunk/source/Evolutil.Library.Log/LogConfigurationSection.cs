using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Evolutil.Library.Log
{   
    /// <summary>Define a Log section.
    /// The LogSection type allows to define a custom section 
    /// programmatically.
    /// </summary>
    internal sealed class LogConfigurationSection : ConfigurationSection, ILogConfigurationSection
    {        
        // The collection (property bag) that contains 
        // the section properties.
        private static ConfigurationPropertyCollection _Properties;

        // Internal flag to disable property setting.
        private static bool _ReadOnly;
        
        /// <summary>
        /// The FileName property.
        /// </summary>
        private static readonly ConfigurationProperty _FileName =
            new ConfigurationProperty("fileName",
            typeof(string), "default.log",
            ConfigurationPropertyOptions.IsRequired);

        /// <summary>
        /// The Severity property.
        /// </summary>
        private static readonly ConfigurationProperty _Severity =
            new ConfigurationProperty("severity", typeof (string), 
                Enum.GetName(typeof(LogSeverity), LogSeverity.Info),
            ConfigurationPropertyOptions.IsRequired);

        /// <summary>
        /// The Eventlog property.
        /// </summary>
        private static readonly ConfigurationProperty _Eventlog =
            new ConfigurationProperty("eventlog", typeof(bool),
                false, ConfigurationPropertyOptions.None);        

        /// <summary>
        /// LogSection constructor.
        /// </summary>
        public LogConfigurationSection()
        {
            // Property initialization
            _Properties = new ConfigurationPropertyCollection { _FileName, _Severity, _Eventlog };
        }
        
        /// <summary>
        ///This is a key customization. 
        /// It returns the initialized property bag.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }


        private new static bool IsReadOnly
        {
            get
            {
                return _ReadOnly;
            }
        }

        /// <summary>
        /// Use this to disable property setting.
        /// </summary>
        /// <param name="propertyName"></param>
        private static void ThrowIfReadOnly(string propertyName)
        {
            if (IsReadOnly)
                throw new ConfigurationErrorsException(
                    "The property " + propertyName + " is read only.");
        }


        /// <summary>
        /// Customizes the use of CustomSection
        /// by setting _ReadOnly to false.
        /// Remember you must use it along with ThrowIfReadOnly.
        /// </summary>
        /// <returns></returns>
        protected override object GetRuntimeObject()
        {
            // To enable property setting just assign true to
            // the following flag.
            _ReadOnly = true;
            return base.GetRuntimeObject();
        }


        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\",
            MinLength = 1, MaxLength = 60)]
        public string FileName
        {
            get
            {
                return (string)this["fileName"];
            }
            set
            {
                // With this you disable the setting.
                // Remember that the _ReadOnly flag must
                // be set to true in the GetRuntimeObject.
                ThrowIfReadOnly("FileName");
                this["fileName"] = value;
            }
        }

        public string Severity
        {
            get
            {
                return (string)this["severity"];
            }
            set
            {
                // With this you disable the setting.
                // Remember that the _ReadOnly flag must
                // be set to true in the GetRuntimeObject.
                ThrowIfReadOnly("severity");
                this["severity"] = value;
            }
        }

        public bool Eventlog
        {
            get
            {
                return (bool)this["eventlog"];
            }
            set
            {
                // With this you disable the setting.
                // Remember that the _ReadOnly flag must
                // be set to true in the GetRuntimeObject.
                ThrowIfReadOnly("eventlog");
                this["eventlog"] = value;
            }
        }

        public void Configure(IUnityContainer container)
        {
            container.RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<LogConfigurationSection>(this);
        }
        public void Configure(IUnityContainer container, string logger)
        {
            container.RegisterType<ILogger, Logger>(logger, new ContainerControlledLifetimeManager());
            container.RegisterInstance<LogConfigurationSection>(this);
        }
    }

}