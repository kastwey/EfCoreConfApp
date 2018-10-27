
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Entities;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class AutoDetectChangesEnabledCommand : DbCommand, IAutoDetectChangesEnabledCommand
	{
		public AutoDetectChangesEnabledCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var vendors = GetVendors();

			try
			{
				dataContext.ChangeTracker.AutoDetectChangesEnabled = false;
				vendors.ForEach(x => dataContext.Add(x));
				await dataContext.SaveChangesAsync(cancellationToken);
			}
			finally
			{
				dataContext.ChangeTracker.AutoDetectChangesEnabled = true;
			}
		}

		private List<Vendor> GetVendors()
		{
			var vendors = new List<Vendor>();
			for (int i = 0; i < 1000; i++)
			{
				vendors.Add(new Vendor { Name = $"New vendor: {i}" });
			}
			return vendors;
		}
	}
}
