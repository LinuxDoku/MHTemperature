using System;
using System.Threading;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Infrastructure {
    public abstract class ScheduledServiceBase : IService {
        private CancellationTokenSource _cancellationTokenSource;

        protected abstract TimeSpan Interval { get; }
        protected abstract void Execute();

        public void Start() {
            _cancellationTokenSource = Scheduler.Interval(Interval, Execute);
        }

        public void Stop() {
            _cancellationTokenSource.Cancel();
        }
    }
}