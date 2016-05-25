using System.Data.Entity;

namespace MHTemperature.Service.Data.Context {
    public abstract class ContextBase<T> where T : class {
        protected Database Db { get; }

        protected ContextBase() {
            Db = new Database();
        }

        protected abstract DbSet<T> DbSet { get; }

        /// <summary>
        /// Delete all entities from this collection.
        /// </summary>
        public void Clear() {
            DbSet.RemoveRange(DbSet);
        }

        /// <summary>
        /// Save entity to collection.
        /// </summary>
        /// <param name="entity"></param>
        public void Save(T entity) {
            DbSet.Add(entity);
            Db.SaveChanges();
        }
    }
}