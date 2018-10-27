
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProductReviewsLazyLoadCommand : DbCommand, IProductReviewsLazyLoadCommand
	{
		public ProductReviewsLazyLoadCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var products = await dataContext.Products
				.Where(x => x.Reviews.Any())
                .OrderBy(x=>x.Name)
                .Take(5)
				.ToListAsync(cancellationToken);

			foreach (var product in products)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new TaskCanceledException("Cancelation requested while iterating to load lazy properties.");
				}
				var voterName = product.Reviews?.Select(x => x.VoterName);
			}
		}
	}
}
