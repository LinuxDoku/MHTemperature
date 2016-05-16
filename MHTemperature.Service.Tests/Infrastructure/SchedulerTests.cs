using System;
using MHTemperature.Service.Infrastructure;
using NUnit.Framework;
using System.Threading;

namespace MHTemperature.Service.Tests.Infrastructure {
    [TestFixture]
    public class SchedulerTests {
        [Test]
        public void Should_Run_Scheduled_Task() {
            var executed = 0;
            var autoResetEvent = new AutoResetEvent(false);

            var cancellationToken = Scheduler.Interval(() => TimeSpan.FromMilliseconds(100), () => {
                executed++;
            }, TimeSpan.FromMilliseconds(2 * 100));

            autoResetEvent.WaitOne(TimeSpan.FromMilliseconds(20 * 100));
            cancellationToken.Cancel();

            // 17 not 18, cause thread pool has some overhead
            Assert.AreEqual(17, executed);
        }
    }
}