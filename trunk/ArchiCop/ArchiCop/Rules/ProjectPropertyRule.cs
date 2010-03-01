using Microsoft.Build.Utilities;

namespace ArchiCop.Rules
{
    public class ProjectPropertyRule
    {
        public string PropertyName { get; set; }
        public string PropertyPattern { get; set; }
    }
}
