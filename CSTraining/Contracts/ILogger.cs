using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILogger
    {
        void LogException(string message, Exception exception);
        void LogDebugInfo(string message);
        void Log(string message);
    }
}
