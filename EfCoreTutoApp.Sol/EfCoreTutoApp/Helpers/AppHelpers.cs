
namespace EfCoreTutoApp
{
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public static class AppHelpers
    {
        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
