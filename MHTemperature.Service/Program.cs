using System;
using MHTemperature.Service.Extensions;
using Topshelf;

namespace MHTemperature.Service {
    public class Program {
        public static void Main(string[] args) {
            AppDomain.CurrentDomain.SetupLogging();

            HostFactory.Run(host => {
                host.Service<TemperatureCrawlService>(setup => {
                    setup.ConstructUsing(name => new TemperatureCrawlService());

                    setup.WhenStarted(service => service.Start());
                    setup.WhenStopped(service => service.Stop());
                });

                host.SetServiceName("MHTemperatureService");
                host.SetDisplayName("MHTemperature Service");
                host.SetDescription("Stellt Schnittstellen zur Auswertung der Freibad Temperatur zur Verfügung.");

                host.RunAsLocalSystem();
                host.StartAutomatically();

                host.EnableServiceRecovery(recovery => {
                    recovery.OnCrashOnly();
                    recovery.RestartService(1);
                });
            });
        }
    }
}
