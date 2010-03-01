using Microsoft.Build.Utilities;

namespace ArchiCop.Rules
{
    public static class LogHelpers
    {
        public static void Log(this ProjectProperty rule, TaskLoggingHelper loggingHelper, object propertyNameValue)
        {
            string message = string.Format(rule.Message, rule.RuleType, rule.PropertyName, propertyNameValue);
            if (message.Length>0)
            {
                switch (rule.RuleType)
                {
                    case ArchiCopRuleType.Exclude:
                        loggingHelper.LogMessage(message);
                        break;
                    case ArchiCopRuleType.Invalid:
                        loggingHelper.LogError(message);
                        break;
                    case ArchiCopRuleType.Valid:
                        loggingHelper.LogMessage(message);
                        break;
                    case ArchiCopRuleType.Warning:
                        loggingHelper.LogWarning(message);
                        break;
                }                
            }            
        }
    }
    
}
