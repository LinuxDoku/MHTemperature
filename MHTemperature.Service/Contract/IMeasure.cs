using System;

namespace MHTemperature.Service.Contract {
    public interface IMeasure {
        DateTime MeasuredAt { get; }
    }
}