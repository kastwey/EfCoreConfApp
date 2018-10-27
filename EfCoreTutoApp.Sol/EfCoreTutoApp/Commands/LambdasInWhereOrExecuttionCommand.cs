
namespace EfCoreTutoApp.Commands
{
    using EfCoreTutoApp.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class LambdasInWhereOrExecuttionCommand : DbCommand, ILambdasInWhereOrExecuttionCommand
	{

		public LambdasInWhereOrExecuttionCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		public bool UseLambdaInWhere { get; set; }

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
			if (UseLambdaInWhere)
			{
				var product = await dataContext.Products.Where(p => EF.Functions.Like(p.Name, "color")).FirstOrDefaultAsync();
			}
			else
			{
				var product = await dataContext.Products.FirstOrDefaultAsync(p => EF.Functions.Like(p.Name, "color"));
			}
		}
	}
}