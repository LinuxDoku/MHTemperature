using System;
using MHTemperature.Contracts;
using Residata.Platform.Contract.Data.Attribute;

namespace MHTemperature.Service.Model {
    [TableName("Temperatures")]
    public class Temperature : ITemperature {
        public int Id { get; set; }

        public float Swimmer { get; set; }
        public float NonSwimmer { get; set; }
        public float KidSplash { get; set; }
        public DateTime DateTime { get; set; }

        public DateTime SavedAt { get; set; }

        public static Temperature CreateFrom(ITemperature temperature) {
            return new Temperature {
                DateTime = temperature.DateTime,
                Swimmer = temperature.Swimmer,
                NonSwimmer = temperature.NonSwimmer,
                KidSplash = temperature.KidSplash
            };
        }
    }
}