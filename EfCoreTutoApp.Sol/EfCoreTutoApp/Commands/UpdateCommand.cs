
namespace EfCoreTutoApp.Commands
{
	using EfCoreTutoApp.Abstractions;
	using EfCoreTutoApp.Entities;
    using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Threading;
    using System.Threading.Tasks;

    public class UpdateCommand : DbCommand, IUpdateCommand
	{
        public UpdateCommand(ILoggerFactory loggerFactory, string connectionString, bool enableProxies = false) : base(loggerFactory, connectionString, enableProxies)
		{
        }

        protected override async Task RunActionAsync(CancellationToken cancellationToken)
        {
            var vendor = await dataContext.Vendors.FirstOrDefaultAsync(cancellationToken);

            var fasterUpdate = await dataContext.FindAsync<Vendor>(new object[] { vendor.Id }, cancellationToken);
            fasterUpdate.Name = "Mortys";
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
