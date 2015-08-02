using Topshelf;

namespace MHTemperature.Service {
    public class Program {
        public static void Main(string[] args) {
            HostFactory.Run(x => {
                x.Service<Service>();

                x.SetServiceName("MHTemperature.Service");
                x.SetDisplayName("Freibad Marienhöhe Temperatur Service");

                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.EnableServiceRecovery(y => {
                    y.RestartService(5);
                });
            });
        }
    }
}
