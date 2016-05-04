using System;

namespace MHTemperature.Contracts {
    public interface ITemperatureService {
        /// <summary>
        /// Get the current temperature.
        /// </summary>
        ITemperature Current();
    }
}