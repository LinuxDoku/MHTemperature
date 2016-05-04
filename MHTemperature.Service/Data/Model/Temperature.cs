using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MHTemperature.Contracts;

namespace MHTemperature.Service.Data.Model {
    [Table("temperatures", Schema="public")]
    public class Temperature : ITemperature {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("swimmer")]
        public float Swimmer { get; set; }

        [Column("non_swimmer")]
        public float NonSwimmer { get; set; }

        [Column("kids_splash")]
        public float KidSplash { get; set; }

        [Column("measured_at")]
        public DateTime MeasuredAt { get; set; }

        public static Temperature CreateFrom(ITemperature temperature) {
            return new Temperature {
                Swimmer = temperature.Swimmer,
                NonSwimmer = temperature.NonSwimmer,
                KidSplash = temperature.KidSplash,
                MeasuredAt = temperature.MeasuredAt
            };
        }

        public override string ToString() {
            return $"{MeasuredAt}: Swimmer {Swimmer}, NonSwimmer: {NonSwimmer}, KidsSplash: {KidSplash}";
        }
    }
}