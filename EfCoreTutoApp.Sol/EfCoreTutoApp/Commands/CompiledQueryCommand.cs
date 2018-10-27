
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.DataAccess.Context;
    using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CompiledQueryCommand : DbCommand, ICompiledQueryCommand
	{

        private static readonly Func<DataContext, Guid, Product> _compiledQuery
            = EF.CompileQuery((DataContext context, Guid id)
                => context.Products.Single(x => x.Id == id));


        public CompiledQueryCommand(ILoggerFactory loggerFActory,  string connectionString, bool lazyEnabled = false) : base(loggerFActory, connectionString, lazyEnabled)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
            await Task.Factory.StartNew(GetProducts, cancellationToken);
        }

        private void GetProducts(object taskState)
        {
            var productIds = GetProductsId(10000);
            var token = (CancellationToken)taskState;

            foreach (var id in productIds)
            {
                if (token.IsCancellationRequested) break;
                var product = _compiledQuery(dataContext, id);
            }
        }

        private IEnumerable<Guid> GetProductsId(int count) => 
            dataContext.Products.OrderBy(x => x.Name).Select(x => x.Id).Take(count).ToList();
	}
}
