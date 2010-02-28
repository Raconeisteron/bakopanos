using System.Collections.Generic;
using Microsoft.Build.Framework;
using System.Text.RegularExpressions;

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

        private List<ProjectPropertyRule> _rules;

        public override void Init()
        {
            string cmdText = "Select PropertyName, PropertyPattern From " + Table + "";
            _rules = DataHelper.GetData<ProjectPropertyRule>(ProviderName, ConnectionString, cmdText);           
        }

        public override void CheckProject(VisualStudioProject project)
        {
            foreach (ProjectPropertyRule rule in _rules)
            {
                string nameValue = DataMapper.GetPropertyValue(project, rule.PropertyName).ToString();
                string patternValue = DataMapper.GetPropertyValue(project, rule.PropertyName).ToString();

                bool retValue = Regex.IsMatch(nameValue, rule.PropertyPattern);
                if (retValue == false)
                {
                    Log.LogError("{0} doesn't match regex pattern {1}", rule.PropertyName, rule.PropertyPattern);
                }
            }
        }

    }
}
