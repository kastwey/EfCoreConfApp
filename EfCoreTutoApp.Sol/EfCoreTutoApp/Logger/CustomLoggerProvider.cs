
namespace EfCoreTutoApp
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class CustomLoggerProvider : ILoggerProvider
    {

        public CustomLoggerProvider()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
            // left empty
        }
    }
}
