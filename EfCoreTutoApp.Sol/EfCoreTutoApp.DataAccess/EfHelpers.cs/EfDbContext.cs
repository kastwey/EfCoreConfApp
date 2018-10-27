
namespace EfCoreTutoApp.DataAccess.EfHelperscs
{
    using EfCoreTutoApp.DataAccess.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
	using Microsoft.Extensions.Logging;

	public static class EfDbContext
	{
		public static DbContextOptions<DataContext> SetupOptionsWithConnectionString(ILoggerFactory loggerFactory, string connectionString,
			bool useLazyLoadingProxies = false, bool enableClientWarningException = false)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
			optionsBuilder.UseSqlServer(connectionString);
			if (loggerFactory != null)
			{
				optionsBuilder.UseLoggerFactory(loggerFactory);
			}
			if (useLazyLoadingProxies)
				optionsBuilder.UseLazyLoadingProxies();

			if (enableClientWarningException)
				optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));

			optionsBuilder.EnableSensitiveDataLogging(true);

			return optionsBuilder.Options;
		}
	}
}
