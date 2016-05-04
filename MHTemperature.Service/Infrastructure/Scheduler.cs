using System;
using System.Threading;
using System.Threading.Tasks;

namespace MHTemperature.Service.Infrastructure {
    public class Scheduler {
        /// <summary>
        /// Execute the action in the given interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static CancellationTokenSource Interval(TimeSpan interval, Action action) {
            var canellationToken = new CancellationTokenSource();
            Action wrappedAction = null;

            wrappedAction = async () => {
                action();
                await Task.Delay(interval, canellationToken.Token);
                await Task.Run(wrappedAction, canellationToken.Token);
            };

            Task.Run(wrappedAction, canellationToken.Token);

            return canellationToken;
        }
    }
}