
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Threading;
    using System.Threading.Tasks;

    public class FromSQLProductReviewsMostValuableProductsCommand : DbCommand, IFromSQLProductReviewsMostValuableProductsCommand
	{

		public FromSQLProductReviewsMostValuableProductsCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			var products = await dataContext.Products.FromSql("select p.* from Products p where (select avg(NumStars) from Reviews where ProductId=p.Id) >= 4").ToListAsync(cancellationToken); ;
		}
	}
}
