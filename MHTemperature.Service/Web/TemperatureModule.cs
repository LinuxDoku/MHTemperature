using System;
using System.Linq;
using MHTemperature.Service.Data;
using Nancy;
using Newtonsoft.Json;

namespace MHTemperature.Service.Web {
    public class TemperatureModule : NancyModule {
        /// <summary>
        /// Database Connection Factory.
        /// </summary>
        private Database Db => new Database();

        public TemperatureModule() {
            Get["/temperature"] = x => Json(Temperature());
            Get["/temperature/{id:int}"] = x => Json(Temperature((int)x.id));
            Get["/temperature/{measuredAt:datetime}"] = x => Json(Temperature((DateTime)x.measuredAt));

            Get["/temperatures"] = x => Json(Temperatures());
            Get["/temperatures/today"] = x => Json(Temperatures(DateTime.Today, DateTime.Today.AddDays(1)));
            Get["/temperatures/yesterday"] = x => Json(Temperatures(DateTime.Today.AddDays(-1), DateTime.Today));
            Get["/temperatures/{from:datetime}/{to:datetime}"] = x => Json(Temperatures((DateTime)x.from, (DateTime)x.to));
        }

        public object Temperature() {
            return Db.Temperatures
                    .OrderByDescending(x => x.MeasuredAt)
                    .FirstOrDefault();
        }

        public object Temperature(int id) {
            return Db.Temperatures
                    .FirstOrDefault(x => x.Id == id);
        }

        public object Temperature(DateTime measuredAt) {
            return Db.Temperatures
                    .FirstOrDefault(x => x.MeasuredAt == measuredAt);
        }

        public object Temperatures() {
            return Db.Temperatures
                    .OrderBy(x => x.MeasuredAt)
                    .GroupBy(x => x.MeasuredAt)
                    .Select(x => x.FirstOrDefault());
        }

        public object Temperatures(DateTime from, DateTime to) {
            return Db.Temperatures
                    .Where(x => x.MeasuredAt >= from && x.MeasuredAt <= to)
                    .OrderBy(x => x.MeasuredAt)
                    .GroupBy(x => x.MeasuredAt)
                    .Select(x => x.FirstOrDefault());
        }

        private Response Json(object obj) {
            var response = (Response) JsonConvert.SerializeObject(obj);
            response.ContentType = "application/json";
            return response;
        }
    }
}