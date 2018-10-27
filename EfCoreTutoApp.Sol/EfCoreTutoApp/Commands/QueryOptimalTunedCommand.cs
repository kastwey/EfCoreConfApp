
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Dtos;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class QueryOptimalTunedCommand : DbCommand, IQueryOptimalTunedCommand
	{
		public QueryOptimalTunedCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var products = await dataContext.Products
				.AsNoTracking()
				.Where(x => x.Reviews.Any())
				.Select(x => new ProductListDto
				{
					Id = x.Id,
					Name = x.Name,
					PublishedOn = x.PublishedOn,
					VendorsNames = string.Join(",", x.Reviews.Select(y => y.VoterName).ToList()), //New in EF Core 2.1
					ReviewsCount = x.Reviews.Count,
					ReviewsAverage = x.Reviews.Select(y => (double?)y.NumStars).Average() // Translate to SQL AVG command
				})
			.ToListAsync(cancellationToken);
		}
	}
}
