using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ArchiCop.Rules
{
    public class CheckProjectProperties : ArchiCopRule
    {        
        private List<ProjectPropertyRule> _rules;

        public override void Init(string table, string providerName, string connectionString)
        {
            string cmdText = "Select PropertyName, PropertyPattern From " + table + "";
            _rules = DataHelper.GetData<ProjectPropertyRule>(providerName, connectionString, cmdText);
        }

        public override void CheckProject(VisualStudioProject project)                    
        {            
            foreach (ProjectPropertyRule rule in _rules)
            {
                string nameValue = DataMapper.GetPropertyValue(project, rule.PropertyName).ToString();
                
                bool retValue = Regex.IsMatch(nameValue, rule.PropertyPattern);
                if (retValue == false)
                {
                    Log.LogError("{0} doesn't match regex pattern {1}", rule.PropertyName, rule.PropertyPattern);
                }
            }
        }

    }
}
