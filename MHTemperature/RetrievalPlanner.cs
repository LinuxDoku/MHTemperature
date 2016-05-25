using System;
using MHTemperature.Contracts;

namespace MHTemperature {
    public static class RetrievalPlanner {
        public static TimeSpan Next(ITemperature lastTemperature, DateTime now) {
            // when no temperature was stored yet
            if (lastTemperature == null) {
                return TimeSpan.Zero;
            }

            // when it's after 20:00 and the last temperature is older than this day
            if (now.Hour >= 20 && lastTemperature.MeasuredAt.Date < now.Date) {
                return TimeSpan.Zero;
            }

            // when it's after 20:00, plan for tomorrow 6:53
            if (now.Hour >= 20) {
                return now.Date.AddDays(1).AddHours(6).AddMinutes(53) - now;
            }

            // when it's before 6:53, plan for this date
            if (now.Hour <= 6 && now.Minute < 53) {
                return now.Date.AddHours(6).AddMinutes(53) - now;
            }

            // when last temperature is older than 1 hour
            if ((now - lastTemperature.MeasuredAt) > TimeSpan.FromHours(1)) {
                return TimeSpan.Zero;
            }

            // plan next retreival for next hour at XX:53
            if (now.Minute > 53) {
                return now.Date.AddHours(now.Hour + 1).AddMinutes(53) - now;
            }

            // this hour at XX:53
            return now.Date.AddHours(now.Hour).AddMinutes(53) - now;
        }
    }
}