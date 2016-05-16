using System;
using System.Threading;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Infrastructure {
    public abstract class ScheduledServiceBase : IService {
        private CancellationTokenSource _cancellationTokenSource;

        protected abstract string Name { get; }
        protected abstract TimeSpan PlanNextExecution();
        protected abstract void Execute();

        public void Start() {
            _cancellationTokenSource = Scheduler.Interval(Log(PlanNextExecution), Execute, Log(PlanNextExecution)());
        }

        public void Stop() {
            _cancellationTokenSource.Cancel();
        }

        protected Func<TimeSpan> Log(Func<TimeSpan> planNextExecution) {
            return () => {
                var next = planNextExecution();

                ServiceStatus.Instance.Status[$"{Name}.NextExecution"] = DateTime.Now.Add(next);

                return next;
            };
        }
    }
}