using System;
using System.Threading;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Infrastructure {
    public abstract class ScheduledServiceBase : IService {
        private CancellationTokenSource _cancellationTokenSource;

        protected abstract TimeSpan PlanNextExecution();

        protected abstract void Execute();

        public void Start() {
            _cancellationTokenSource = Scheduler.Interval(PlanNextExecution, Execute, PlanNextExecution());
        }

        public void Stop() {
            _cancellationTokenSource.Cancel();
        }
    }
}