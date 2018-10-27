
namespace EfCoreTutoApp.Commands
{
    using Dapper;
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.DataAccess.Context;
    using EfCoreTutoApp.DataAccess.EfHelperscs;
    using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetProductsWithMoreThanFiveReviewsCommand : DbCommand, IGetProductsWithMoreThanFiveReviewsCommand
	{

		private readonly ILoggerFactory _loggerFactory;

		public bool UseDapper { get; set; }

		public GetProductsWithMoreThanFiveReviewsCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
			_loggerFactory = loggerFactory;
		}

		protected override async Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken)
		{
			if (!UseDapper)
			{
				using (dataContext = new DataContext(EfDbContext.SetupOptionsWithConnectionString(_loggerFactory, _connectionString, EnableProxies)))
				{
					var products = await dataContext.Products.FromSql("select p.* from Products p where (select count(1) from Reviews r where r.ProductId = p.Id) > 5").ToListAsync(cancellationToken);

				}
			}
			else
			{
				using (var connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					var query = "select p.* from Products p where (select count(1) from Reviews r where r.ProductId=p.Id) > 5";
					var stopwatch = Stopwatch.StartNew();
					Console.WriteLine("Dapper. Executing query: \"" + query + "\"");
					var products = await connection.QueryAsync(new CommandDefinition(query, cancellationToken: cancellationToken ));
					stopwatch.Stop();
					Console.WriteLine("Dapper: command executed in " + stopwatch.Elapsed.TotalMilliseconds.ToString() + " MS.");
				}
			}
		}
		protected override Task RunActionAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
