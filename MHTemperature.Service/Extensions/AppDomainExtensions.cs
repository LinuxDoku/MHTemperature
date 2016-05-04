using System;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service.Extensions {
    public static class AppDomainExtensions {
        /// <summary>
        /// Setup logging for AppDomain.
        /// </summary>
        /// <param name="appDomain"></param>
        public static void SetupLogging(this AppDomain appDomain) {
            appDomain.UnhandledException += (sender, eventArgs) => {
                Logger.Error("Unhandled exception occurred!", eventArgs.ExceptionObject as Exception);
            };
        }
    }
}