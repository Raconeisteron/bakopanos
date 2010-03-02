using Microsoft.Build.Utilities;

namespace ArchiCop.Rules
{

    public class ProjectProperty
    {
        public string PropertyName { get; set; }
        public string PropertyPattern { get; set; }
        public ArchiCopRuleType RuleType { get; set; }
        public string Message { get; set; }

    }        
}
