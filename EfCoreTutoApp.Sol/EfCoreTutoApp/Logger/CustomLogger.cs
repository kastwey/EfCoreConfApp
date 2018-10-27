
namespace EfCoreTutoApp
{
	using Microsoft.Extensions.Logging;
	using System;
	using System.Collections.Generic;

	public class CustomLogger : ILogger
	{

		public CustomLogger()
		{
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel >= LogLevel.Information;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var logMessage = formatter(state, exception);
			Console.WriteLine(logMessage);
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}
	}
}
