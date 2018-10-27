
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Extensions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class ReviewsProductSelectLoadCommand : DbCommand, IReviewsProductSelectLoadCommand
	{
		public ReviewsProductSelectLoadCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var productId = await dataContext.Products
				.AsNoTracking()
				.Where(x => x.Reviews.Count > 1)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

			var reviews = await dataContext.Reviews
				.AsNoTracking()
				.Where(x => x.Product.Id == productId)
				.ToReviewDtoList()
				.ToListAsync(cancellationToken);
		}
	}
}
