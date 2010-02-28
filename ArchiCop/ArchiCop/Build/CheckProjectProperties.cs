using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data;
namespace ArchiCop.Build
{
    public class ProjectPropertyRule
    {
        public string PropertyName { get; set; }
        public string PropertyPattern { get; set; }
    }

    public class CheckProjectProperties : CheckVisualStudioProjectList
    {
        [Required]
        public string ConnectionString { get; set; }
        
        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string Table { get; set; }

        public override void CheckProject(VisualStudioProject project)
        {
            string cmdText = "Select PropertyName, PropertyPattern From [" + Table + "]";
            List<ProjectPropertyRule> rules = 
                DataHelper.GetData<ProjectPropertyRule>(ProviderName, ConnectionString, cmdText);
            
            foreach (ProjectPropertyRule rule in rules)
            {
                string name = rule.PropertyName;
                string pattern = rule.PropertyPattern;

                string value = DataMapper.GetPropertyValue(project, name).ToString();
                bool retValue = Regex.IsMatch(value, pattern);
                if (retValue == false)
                {
                    Log.LogError("{0} doesn't match regex pattern {1}", name, pattern);
                }                      
            }            
        }

    }
}
