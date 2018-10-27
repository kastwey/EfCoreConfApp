
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserDefinedFunctionCommand : DbCommand, IUserDefinedFunctionCommand
	{

		public bool UseUDF { get; set; }


		public UserDefinedFunctionCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			if (UseUDF)
			{
				var productsWithAverage = await dataContext.Products.Where(p => p.Reviews.Any()).Select(p => new { p.Id, p.Name, VoteAverage = dataContext.UDFAverageVotes(p.Id) }).ToListAsync(cancellationToken);
			}
			else
			{
				var productsWithAverage = await (from p in dataContext.Products join rv in dataContext.Reviews on p.Id equals rv.ProductId group rv by new { rv.Product.Id, rv.Product.Name } into rvgp select new { rvgp.Key.Id, rvgp.Key.Name, Average = rvgp.Average(r => r.NumStars) }).ToListAsync(cancellationToken);
				// var productsWithAverage = await dataContext.Products.Where(p => p.Reviews.Any()).Select(p => new { p.Id, p.Name, VoteAverage = p.Reviews.Select(rv => rv.NumStars).Average() }).ToListAsync(cancellationToken);
			}
		}
	}
}
