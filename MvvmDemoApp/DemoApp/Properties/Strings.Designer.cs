﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoApp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DemoApp.Properties.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All Customers.
        /// </summary>
        public static string AllCustomersViewModel_DisplayName {
            get {
                return ResourceManager.GetString("AllCustomersViewModel_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Company.
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_Company {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_Company", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Not Specified).
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_NotSpecified {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_NotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Person.
        /// </summary>
        public static string CustomerViewModel_CustomerTypeOption_Person {
            get {
                return ResourceManager.GetString("CustomerViewModel_CustomerTypeOption_Person", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New Customer.
        /// </summary>
        public static string CustomerViewModel_DisplayName {
            get {
                return ResourceManager.GetString("CustomerViewModel_DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Customer type must be selected.
        /// </summary>
        public static string CustomerViewModel_Error_MissingCustomerType {
            get {
                return ResourceManager.GetString("CustomerViewModel_Error_MissingCustomerType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot save an invalid customer..
        /// </summary>
        public static string CustomerViewModel_Exception_CannotSave {
            get {
                return ResourceManager.GetString("CustomerViewModel_Exception_CannotSave", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create new customer.
        /// </summary>
        public static string MainWindowViewModel_Command_CreateNewCustomer {
            get {
                return ResourceManager.GetString("MainWindowViewModel_Command_CreateNewCustomer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to View all customers.
        /// </summary>
        public static string MainWindowViewModel_Command_ViewAllCustomers {
            get {
                return ResourceManager.GetString("MainWindowViewModel_Command_ViewAllCustomers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MVVM Demo App.
        /// </summary>
        public static string MainWindowViewModel_DisplayName {
            get {
                return ResourceManager.GetString("MainWindowViewModel_DisplayName", resourceCulture);
            }
        }
    }
}
