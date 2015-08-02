using System;
using System.Collections.Generic;
using System.Linq;
using MHTemperature.Contracts;
using Residata.Platform.Contract.Data;
using Residata.Platform.Contract.Dependency.Attribute;
using Residata.Platform.Contract.Persistence;
using Residata.Platform.Core.Configuration;
using Residata.Platform.Data;

namespace MHTemperature.Service.Domain {
    [Export, Shared]
    public class StorageService {
        private readonly DatabaseManager _databaseManager;
        private readonly IDataContext<Model.Temperature> _dataContext; 

        public StorageService() {
            _databaseManager = new DatabaseManager(new DatabaseConfiguration {
                Adapter = DatabaseAdapter.MySql,
                Uri = "localhost",
                Username = "root",
                Password = "root"
            });

            // create database
            _databaseManager.Database.CreateDatabase("freibadmh");
            _databaseManager.Database.ChangeDatabase("freibadmh");

            // create table initial
            var table = _databaseManager.Models.GetTable<Model.Temperature>();
            _databaseManager.Database.CreateTable(table);

            // create context
            _dataContext = _databaseManager.CreateContext<Model.Temperature>();
        }

        public bool Save(ITemperature temperature) {
            var model = Model.Temperature.CreateFrom(temperature);
            return _dataContext.Save(model) != null;
        }

        public IEnumerable<Model.Temperature> GetToday() {
            return GetDay(DateTime.Today.AddDays(-1), DateTime.Today);
        }

        public IEnumerable<Model.Temperature> GetYesterday() {
            return GetDay(DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1));
        }

        public IEnumerable<Model.Temperature> GetDay(DateTime day) {
            return GetDay(day.AddDays(-1), day);
        } 

        public IEnumerable<Model.Temperature> GetDay(DateTime start, DateTime end) {
            return _dataContext.Find().Where(x => x.DateTime > start && x.DateTime <= end)
                                      .OrderBy(x => x.DateTime);
        } 
    }
}