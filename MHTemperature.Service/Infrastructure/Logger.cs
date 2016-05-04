using System;
using System.Diagnostics;

namespace MHTemperature.Service.Infrastructure {
    /// <summary>
    /// Logger, which logs into the windows event log.
    /// </summary>
    public static class Logger {
        static Logger() {
            Trace.Listeners.Add(new EventLogTraceListener("MHTemperature Service"));

            if (Environment.UserInteractive) {
                Trace.Listeners.Add(new ConsoleTraceListener());
            }
        }

        public static void Info(string message) {
            Trace.TraceInformation(message);
        }

        public static void Warn(string message) {
            Trace.TraceWarning(message);
        }

        public static void Error(string message, Exception exception = null) {
            Trace.TraceError("{0}{1}{2}", message, Environment.NewLine, exception);
        }
    }
}