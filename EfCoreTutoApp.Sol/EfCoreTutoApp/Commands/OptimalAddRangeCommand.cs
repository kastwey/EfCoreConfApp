
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Entities;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;

	public class OptimalAddRangeCommand : DbCommand, IOptimalAddRangeCommand
	{
		public OptimalAddRangeCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var vendors = GetVendors();
			dataContext.AddRange(vendors);
			await dataContext.SaveChangesAsync(cancellationToken);
		}

		private IEnumerable<Vendor> GetVendors()
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
