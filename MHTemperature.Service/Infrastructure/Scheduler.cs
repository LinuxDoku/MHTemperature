using System;
using System.Threading;
using System.Threading.Tasks;

namespace MHTemperature.Service.Infrastructure {
    public static class Scheduler {
        /// <summary>
        /// Execute the action and plan the next execution afterwards.
        /// </summary>
        /// <param name="planNextExecution"></param>
        /// <param name="action"></param>
        /// <param name="initialTimeout"></param>
        /// <returns></returns>
        public static CancellationTokenSource Interval(Func<TimeSpan> planNextExecution, Action action, TimeSpan? initialTimeout=null) {
            var cancellationToken = new CancellationTokenSource();
            Action wrappedAction = null;

            wrappedAction = async () => {
                if (cancellationToken.IsCancellationRequested) {
                    return;
                }

                action();
                await Task.Delay(planNextExecution(), cancellationToken.Token);
                await Task.Run(wrappedAction, cancellationToken.Token);
            };

            if (initialTimeout.HasValue) {
                Task.Run(async () => {
                    await Task.Delay(initialTimeout.Value, cancellationToken.Token);
                    await Task.Run(wrappedAction, cancellationToken.Token);
                }, cancellationToken.Token);
            } else {
                Task.Run(wrappedAction, cancellationToken.Token);
            }

            return cancellationToken;
        }
    }
}