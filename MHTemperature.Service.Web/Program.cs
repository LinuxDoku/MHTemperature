using Topshelf;

namespace MHTemperature.Service.Web {
    public class Program {
        public static void Main(string[] args) {
            HostFactory.Run(x => {
                x.Service<Service>();

                x.SetServiceName("MHTemperature.Service.Web");
                x.SetDisplayName("Freibad Marienhöhe Temperatur Web Service");

                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.EnableServiceRecovery(y => {
                    y.RestartService(5);
                });
            });
        }
    }
}
