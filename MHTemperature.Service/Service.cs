﻿using Residata.Platform.Server.Worker;
using Topshelf;

namespace MHTemperature.Service {
    public class Service : ServiceControl {
        public bool Start(HostControl hostControl) {
            AppHostFactory.CreateAndRun<ServiceApplication>();

            return true;
        }

        public bool Stop(HostControl hostControl) {
            return true;
        }
    }
}