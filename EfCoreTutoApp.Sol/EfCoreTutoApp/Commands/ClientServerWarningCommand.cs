
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class ClientServerWarningCommand : DbCommand, IClientServerWarningCommand
	{
		public ClientServerWarningCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
		}

		protected override async Task RunActionAsync(CancellationToken cancellationToken)
		{
            var result = await Task.FromResult(dataContext.Products
                .AsNoTracking()
                .Where(x => x.Reviews.Any())
                .Take(5)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Price,
                    Voters = string.Join(",", x.Reviews.Select(y => y.VoterName))
                }
            ).OrderBy(x => x.Voters).ToList());
		}
	}
}
