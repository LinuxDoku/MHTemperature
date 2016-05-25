using System;
using System.Collections.Generic;
using System.Linq;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Extensions {
    public static class MeasureEnumerableExtensions {
        public static IEnumerable<T> Between<T>(this IEnumerable<T> enumerable, DateTime from, DateTime to) where T : IMeasure {
            return enumerable.Where(x => x.MeasuredAt >= from && x.MeasuredAt < to);
        }
    }
}