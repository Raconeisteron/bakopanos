using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
