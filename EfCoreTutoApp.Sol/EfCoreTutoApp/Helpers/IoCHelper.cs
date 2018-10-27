
namespace EfCoreTutoApp.Helpers
{
    using EfCoreTutoApp.Abstractions;
    using EfCoreTutoApp.Commands;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class IoCHelper
    {
        public static void ConfigureServices(IServiceCollection serviceCollection, ILoggerFactory loggerFactory, string connectionString)
        {
            serviceCollection.AddTransient(typeof(IAutoDetectChangesEnabledCommand), (provider) => new AutoDetectChangesEnabledCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IClientServerWarningCommand), (provider) => new ClientServerWarningCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(ICompiledQueryCommand), (provider) => new CompiledQueryCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IComputedColumnCommand), (provider) => new ComputedColumnCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IFromSQLProductReviewsMostValuableProductsCommand), (provider) => new FromSQLProductReviewsMostValuableProductsCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IGetProductsWithMoreThanFiveReviewsCommand), (provider) => new GetProductsWithMoreThanFiveReviewsCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(INotCompiledQueryCommand), (provider) => new NotCompiledQueryCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IOptimalAddRangeCommand), (provider) => new OptimalAddRangeCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IProductReviewsLazyLoadCommand), (provider) => new ProductReviewsLazyLoadCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IQueryOptimalTunedCommand), (provider) => new QueryOptimalTunedCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IQueryToTuneCommand), (provider) => new QueryToTuneCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IReviewsProductEagerLoadCommand), (provider) => new ReviewsProductEagerLoadCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IReviewsProductSelectLoadCommand), (provider) => new ReviewsProductSelectLoadCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(ITrackGraphCommand), (provider) => new TrackGraphCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IUpdateCommand), (provider) => new UpdateCommand(loggerFactory, connectionString));
            serviceCollection.AddTransient(typeof(IUserDefinedFunctionCommand), (provider) => new UserDefinedFunctionCommand(loggerFactory, connectionString));
        }
    }
}
