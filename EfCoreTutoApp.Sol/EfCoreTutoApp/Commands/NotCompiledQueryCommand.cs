
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.Extensions.Logging;
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class NotCompiledQueryCommand : DbCommand, INotCompiledQueryCommand
	{
        public NotCompiledQueryCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
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
                var products = dataContext.Products.Single(x => x.Id == id);
            }
        }

        private IEnumerable<Guid> GetProductsId(int count) => 
            dataContext.Products.OrderBy(x => x.Name).Select(x => x.Id).Take(count).ToList();
    }
}