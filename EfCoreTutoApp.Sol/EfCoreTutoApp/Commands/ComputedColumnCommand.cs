
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class ComputedColumnCommand : DbCommand, IComputedColumnCommand
	{
		private const string VendorName = "Bu Monster";

		public ComputedColumnCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var products = await dataContext.Products
				.Where(x => x.ProductVendor
				.Any(y => y.Vendor.Name
				.Contains(VendorName)))
				.ToListAsync(cancellationToken);

			foreach (var product in products)
			{
				product.PublishedOn = new DateTime(2018, 6, 7);
			}

			await dataContext.SaveChangesAsync(cancellationToken);
		}
	}
}
