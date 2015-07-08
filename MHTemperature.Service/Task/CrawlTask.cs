using System;
using System.Threading;
using MHTemperature.Service.Domain;
using Residata.Platform.Contract.Core.Log;
using Residata.Platform.Contract.Dependency.Attribute;
using Residata.Platform.Contract.Task.Attribute;
using Residata.Platform.Core.Task;

namespace MHTemperature.Service.Task {
    [Export, Schedule(EveryMinutes = 15)]
    public class CrawlTask : TaskBase {
        private readonly StorageService _storageService;
        private readonly ILogger _logger;
        private readonly TemperatureService _temperatureService;

        public CrawlTask(StorageService storageService, ILogger logger) {
            _storageService = storageService;
            _logger = logger;
            _temperatureService = new TemperatureService();
        }

        protected override void Execute(CancellationToken cancellationToken) {
            Console.WriteLine("Save Temperature.");

            try {
                var temperature = _temperatureService.Current();
                _storageService.Save(temperature);
            } catch (Exception ex) {
                _logger.Fatal(ex, "Could not save temperature.");
            }
        }
    }
}