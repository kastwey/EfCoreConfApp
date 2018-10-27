
namespace EfCoreTutoApp.Commands
{
    using EfCoreTutoApp.Abstractions;
    using EfCoreTutoApp.DataAccess.Context;
    using EfCoreTutoApp.DataAccess.EfHelperscs;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DbCommand : IDbCommand
	{
		protected DataContext dataContext;
		protected readonly string _connectionString;

		public bool EnableProxies { get; set; }

		private readonly ILoggerFactory _loggerFactory;

		protected DbCommand(ILoggerFactory loggerFactory,  string connectionString, bool enableProxies = false)
		{
			_loggerFactory = loggerFactory;
			_connectionString = connectionString;
			EnableProxies = enableProxies;
		}

		public async Task RunAsync(CancellationToken cancellationToken)
		{
			await ExecuteAsync(async (cToken) => await RunActionAsync(cToken), cancellationToken);
		}

		protected virtual async Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken)
		{
			using (dataContext = new DataContext(EfDbContext.SetupOptionsWithConnectionString(_loggerFactory, _connectionString, EnableProxies)))
			{
				await action(cancellationToken);
			}
		}

		protected abstract Task RunActionAsync(CancellationToken cancellationToken);


	}
}
