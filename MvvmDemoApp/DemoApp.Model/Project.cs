using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DemoApp.Model.Properties;

namespace DemoApp.Model
{
    /// <summary>
    /// Represents a customer of a company.  This class
    /// has built-in validation logic. It is wrapped
    /// by the CustomerViewModel class, which enables it to
    /// be easily displayed and edited by a WPF user interface.
    /// </summary>
    public class Project : IDataErrorInfo
    {
        #region Creation

        protected Project()
        {
        }

        public static Project CreateNewProject()
        {
            return new Project();
        }

        public static Project CreateProject(string projectName)
        {
            return new Project()
                       {
                          ProjectName = projectName
                       };
        }

        #endregion // Creation

        #region State Properties

        /// <summary>
        /// Gets/sets the ProjectName for the project.
        /// </summary>
        public string ProjectName { get; set; }

        #endregion // State Properties

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return null; }
        }

        #endregion

        #region Validation


        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {                
                return true;
            }
        }

        #endregion // Validation
    }
}