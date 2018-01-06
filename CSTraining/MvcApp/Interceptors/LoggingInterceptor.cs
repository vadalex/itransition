using Contracts;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MvcApp.Interceptors
{
    public class LoggingInterceptor : ICallHandler
    {
        private ILogger logger;
        public int Order { get; set; }

        public LoggingInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = getNext().Invoke(input, getNext);
            sw.Stop();
            if (result.Exception != null)
                logger.LogException("oops", result.Exception);
            else
            {
                string message = string.Format("Method name: {0}.{1}; Execution time: {2} ms", input.MethodBase.DeclaringType.ToString(), input.MethodBase.Name, sw.ElapsedMilliseconds);
                this.logger.LogDebugInfo(message);
            } 
            return result;

        }
    }
}
