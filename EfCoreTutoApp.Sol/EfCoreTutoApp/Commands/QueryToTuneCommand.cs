
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Dtos;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class QueryToTuneCommand : DbCommand, IQueryToTuneCommand
	{
		public QueryToTuneCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var products = await dataContext.Products
				.OrderBy(x => x.Name)
				.Where(x => x.Reviews.Any())
				.Select(x => new ProductListDto
				{
					Id = x.Id,
					Name = x.Name,
					PublishedOn = x.PublishedOn,
					VendorsNames = string.Join(",", x.Reviews.Select(y => y.VoterName)),
					ReviewsCount = x.Reviews.Count,
					ReviewsAverage = x.Reviews.Select(y => y.NumStars).Average()
				})
				.Take(10)
			.ToListAsync(cancellationToken);
		}
	}
}
