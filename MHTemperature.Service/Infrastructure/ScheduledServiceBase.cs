using System;
using System.Threading;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Infrastructure {
    public abstract class ScheduledServiceBase : IService {
        private CancellationTokenSource _cancellationTokenSource;

        protected abstract string Name { get; }
        protected abstract TimeSpan MinDelay { get; }

        protected virtual TimeSpan PlanNextExecution() {
            if (LastExecution == default(DateTime)) {
                return TimeSpan.Zero;
            }

            return MinDelay;
        }

        protected abstract void Execute();

        public void Start() {
            _cancellationTokenSource = Scheduler.Interval(
                PlanWrapper(PlanNextExecution),
                ExecuteWrapper(Execute),
                PlanWrapper(PlanNextExecution)()
            );
        }

        public void Stop() {
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Last execution of the service.
        /// </summary>
        public DateTime LastExecution { get; private set; }
        
        private Func<TimeSpan> PlanWrapper(Func<TimeSpan> planNextExecution) {
            return () => {
                var next = planNextExecution();

                if (LastExecution != default(DateTime) && DateTime.Now.Add(MinDelay) > LastExecution.Add(next)) {
                    next = MinDelay;
                }

                ServiceStatus.Instance.Status[$"{Name}.NextExecution"] = DateTime.Now.Add(next);

                return next;
            };
        }

        private Action ExecuteWrapper(Action execute) {
            return () => {
                execute();
                LastExecution = DateTime.Now;
            };
        }
    }
}