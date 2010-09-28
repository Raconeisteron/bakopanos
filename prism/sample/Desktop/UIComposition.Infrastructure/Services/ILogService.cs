// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
namespace UIComposition.Infrastructure.Services
{
    public interface ILogService
    {
        bool IsLoggingEnabled();
        void WriteInfo(object message);
        void WriteWarning(object message);
        void WriteError(object message);
    }
}