using System;

namespace MHTemperature.Contracts {
    public interface ITemperature {
        /// <summary>
        /// Schwimmerbecken.
        /// </summary>
        /// <value>The swimmer pool temperature.</value>
        float Swimmer { get; }

        /// <summary>
        /// Nichtschwimmer- Rutschbecken.
        /// </summary>
        /// <value>The non swimmer pool temperature.</value>
        float NonSwimmer { get; }

        /// <summary>
        /// Kinderplanschbecken.
        /// </summary>
        /// <value>The kid splash pool temperature.</value>
        float KidSplash { get; }

        /// <summary>
        /// Zeitpunkt der Erfassung.
        /// </summary>
        /// <value>DateTime of the temperatures.</value>
        DateTime DateTime { get; }
    }
}