
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Extensions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class ReviewsProductEagerLoadCommand : DbCommand, IReviewsProductEagerLoadCommand
	{
		public ReviewsProductEagerLoadCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var product = await dataContext.Products
				.AsNoTracking()
				.Include(x => x.Reviews)
				.FirstOrDefaultAsync(x => x.Reviews.Count > 2, cancellationToken);

			var reviews = await dataContext.Reviews
				.AsNoTracking()
				.Include(x => x.Product)
				.Where(x => x.ProductId == product.Id)
				.ToReviewDtoListEager()
				.ToListAsync(cancellationToken);
		}
	}
}
