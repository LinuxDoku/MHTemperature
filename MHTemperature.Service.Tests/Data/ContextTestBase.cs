using MHTemperature.Service.Data.Context;
using NUnit.Framework;

namespace MHTemperature.Service.Tests.Data {
    /// <summary>
    /// Base class for all context tests, which clears the context before the test is run.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ContextTestBase<TContext, TEntity> where TContext : ContextBase<TEntity> where TEntity : class {
        protected abstract TContext Context { get; }

        [SetUp]
        public void Setup() {
            Context.Clear();
        }
    }
}