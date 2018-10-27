
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.DataAccess.Context;
	using EfCoreTutoApp.DataAccess.EfHelperscs;
	using EfCoreTutoApp.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class TrackGraphCommand : DbCommand, ITrackGraphCommand
	{

		private readonly ILoggerFactory _loggerFactory;

		public TrackGraphCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
			_loggerFactory = loggerFactory;
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var product = GetFirstProductDisconnected();

			dataContext.ChangeTracker.TrackGraph(product, e =>
			{
				e.Entry.State = EntityState.Unchanged;
				if (e.Entry.Entity is Review)
				{
					e.Entry.Property("NumStars").IsModified = true;
				}
			});
			await dataContext.SaveChangesAsync(cancellationToken);
		}

		private Product GetFirstProductDisconnected()
		{
			using (var db = new DataContext(EfDbContext.SetupOptionsWithConnectionString(_loggerFactory, _connectionString)))
			{
				var product = db.Products.Include(x => x.Reviews)
					.FirstOrDefault(x => x.Reviews.Any());

				foreach (var review in product.Reviews)
				{
					review.NumStars = 5;
				}
				return product;
			}
		}
	}
}
