using MHTemperature.Contracts;

namespace MHTemperature {
    internal class Temperature : ITemperature {
        /// <summary>
        /// Schwimmerbecken.
        /// </summary>
        /// <value>The swimmer pool temperature.</value>
        public float Swimmer { get; set; }

        /// <summary>
        /// Nichtschwimmer- Rutschbecken.
        /// </summary>
        /// <value>The non swimmer pool temperature.</value>
        public float NonSwimmer { get; set; }

        /// <summary>
        /// Kinderplanschbecken.
        /// </summary>
        /// <value>The kid splash pool temperature.</value>
        public float KidSplash { get; set; }

        /// <summary>
        /// Zeitpunkt der Erfassung.
        /// </summary>
        /// <value>DateTime of the temperatures.</value>
        public System.DateTime MeasuredAt { get; set; }
    }
}