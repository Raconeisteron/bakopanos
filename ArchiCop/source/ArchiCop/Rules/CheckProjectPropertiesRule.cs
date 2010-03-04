using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArchiCop.Library;

namespace ArchiCop.Rules
{
    public class CheckProjectPropertiesRule : ArchiCopRule
    {
        private List<ProjectProperty> _rules;

        public override void Init(ArchiCopRuleParams parameters)
        {
            string cmdText = "Select PropertyName, PropertyPattern, RuleType, Message From " + parameters.Table + "";
            _rules = DataHelper.GetData<ProjectProperty>(parameters.ProviderName, parameters.ConnectionString, cmdText);
        }

        public override void CheckProject(VsProject project)
        {
            var propertyNames = (from item in _rules
                                 select item.PropertyName).Distinct();

            foreach (string propertyName in propertyNames)
            {
                string propertyNameValue = DataMapper.GetPropertyValue(project, propertyName).ToString();

                var exclude = from item in _rules
                              where item.PropertyName == propertyName
                              where item.RuleType == ArchiCopRuleType.Exclude
                              select item;

                var valid = from item in _rules
                            where item.PropertyName == propertyName
                            where item.RuleType == ArchiCopRuleType.Valid
                            select item;

                var warning = from item in _rules
                              where item.PropertyName == propertyName
                              where item.RuleType == ArchiCopRuleType.Warning
                              select item;

                var invalid = from item in _rules
                              where item.PropertyName == propertyName
                              where item.RuleType == ArchiCopRuleType.Invalid
                              select item;

                //step 1
                bool isExclude = false;
                foreach (ProjectProperty rule in exclude)
                {
                    bool retValue = Regex.IsMatch(propertyNameValue, rule.PropertyPattern);
                    if (retValue)
                    {
                        isExclude = true;
                        rule.Log(Log, propertyNameValue);                        
                    }
                }

                //step 2
                bool isValid = false;
                if (isExclude == false)
                {
                    foreach (ProjectProperty rule in valid)
                    {
                        bool retValue = Regex.IsMatch(propertyNameValue, rule.PropertyPattern);
                        if (retValue)
                        {
                            isValid = true;
                            rule.Log(Log, propertyNameValue);                        
                        }
                    }
                }

                //step 3
                bool isWarning = false;
                if (isValid == false && isExclude == false)
                {
                    foreach (ProjectProperty rule in warning)
                    {
                        bool retValue = Regex.IsMatch(propertyNameValue, rule.PropertyPattern);
                        if (retValue)
                        {
                            rule.Log(Log, propertyNameValue);                        
                            isWarning = true;
                        }
                    }
                }

                //step 4
                if (isValid == false && isWarning == false && isExclude == false)
                {
                    foreach (ProjectProperty rule in invalid)
                    {
                        bool retValue = Regex.IsMatch(propertyNameValue, rule.PropertyPattern);
                        if (retValue)
                        {
                            rule.Log(Log, propertyNameValue);                        
                        }
                    }
                }
            }


        }        
    }
}
