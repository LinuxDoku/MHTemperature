using System;
using MHTemperature.Service.Extensions;
using MHTemperature.Service.Infrastructure;
using MHTemperature.Service.Service;
using Topshelf;

namespace MHTemperature.Service {
    public static class Program {
        public static void Main(string[] args) {
            AppDomain.CurrentDomain.SetupLogging();

            HostFactory.Run(host => {
                host.Service<ServiceHost>(setup => {
                    setup.ConstructUsing(name => new ServiceHost(
                        new TemperatureCrawlService(),
                        new WeatherCrawlService(),
                        new WebService()
                    ));
                    
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
