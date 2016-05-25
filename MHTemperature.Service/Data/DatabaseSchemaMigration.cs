using System;
using System.IO;
using System.Reflection;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service.Data {
    public static class DatabaseSchemaMigration {
        private static readonly object _migrateLock = new object();
        private static bool _migrated = false;

        public static void Migrate(Database database) {
            lock (_migrateLock) {
                if (_migrated) {
                    return;
                }

                // execute schema script on database
                try {
                    string script;

                    var assembly = Assembly.GetExecutingAssembly();
                    var resourceName = $"{typeof(Database).Namespace}.Sql.Schema.sql";

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream)) {
                        script = reader.ReadToEnd();
                    }

                    if (!string.IsNullOrEmpty(script)) {
                        database.Database.ExecuteSqlCommand(script);
                    }

                    _migrated = true;
                }
                catch (Exception ex) {
                    Logger.Error("Could not migrate database schema!", ex);
                }
            }
        } 
    }
}