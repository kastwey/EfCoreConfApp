
namespace EfCoreTutoApp.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDbCommand
	{
		bool EnableProxies { get; set; }
		Task RunAsync(CancellationToken cancellationToken);
	}
}
