namespace EfCoreTutoApp
{
    using EfCoreTutoApp.Abstractions;
    using EfCoreTutoApp.Commands;
    using EfCoreTutoApp.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Program
	{
		private static string _connectionString;

		private static volatile bool _stop = false;

		private static bool _cancelRequested = false;

		private static CancellationTokenSource _cancellationTokenSource = null;

		private static IServiceProvider _serviceProvider;

		private static ILoggerFactory _loggerFactory;

		private static async Task Main()
		{
			var configuration = AppHelpers.BuildConfiguration();
			var services = new ServiceCollection();
			services.AddLogging();
			ConfigureServices(services);

			_connectionString = configuration["ConnectionStrings:DefaultConnection"];
			_connectionString = ConnStringHelpers.ReplaceVarsFromConnectionString(_connectionString);
			_serviceProvider = services.BuildServiceProvider();
			_loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
			_loggerFactory.AddProvider(new CustomLoggerProvider());

			try
			{
				await PrintAndWaitForOption();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		private static void Console_Cancel(object sender, ConsoleCancelEventArgs e)
		{
			if (_cancelRequested)
			{
				Console.WriteLine("Exitting...");
				_stop = true;
				e.Cancel = true;
			}
			else
			{
				_cancelRequested = true;
				_cancellationTokenSource?.Cancel();
				e.Cancel = true;
				Console.WriteLine("Canceling current execution if any... Press CTL+C again to exit.");
			}
		}

		private static async Task PrintAndWaitForOption()
		{
			Console.CancelKeyPress += Console_Cancel;

			PrintIntroText();
			while (!_stop)
			{
				_cancelRequested = false;
				_cancellationTokenSource = new CancellationTokenSource();
				Console.WriteLine();
				Console.Write(@"EF Core Tuto:\>");
				IDbCommand command = null;
				var option = Console.ReadLine();
				if (_stop)
				{
					break;
				}

				switch (option?.ToLower() ?? string.Empty)
				{
					case "":
						break;
					case "cls":
						Console.Clear();
						break;
					case "opt":
						PrintIntroText();
						break;
					case "1":
						Console.WriteLine("Enable Lazy loading? (y/n)");
						var lazyEnabled = Console.ReadLine() == "y";
						command = _serviceProvider.GetService<IProductReviewsLazyLoadCommand>();
						command.EnableProxies = lazyEnabled;
						break;
					case "2":
						command = _serviceProvider.GetService<IReviewsProductEagerLoadCommand>();
						break;
					case "3":
						command = _serviceProvider.GetService<IReviewsProductSelectLoadCommand>();
						break;
					case "4":
						command = _serviceProvider.GetService<IClientServerWarningCommand>();
						break;
					case "5":
						command = _serviceProvider.GetService<IUpdateCommand>();
						break;
					case "6":
						command = _serviceProvider.GetService<IComputedColumnCommand>();
						break;
					case "7":
						command = _serviceProvider.GetService<ITrackGraphCommand>();
						break;
                    case "8":
						command = _serviceProvider.GetService<INotCompiledQueryCommand>();

						break;
					case "9":
						command = _serviceProvider.GetService<ICompiledQueryCommand>();
						break;
					case "10":
						command = _serviceProvider.GetService<IQueryToTuneCommand>();
						break;
					case "11":
						command = _serviceProvider.GetService <IQueryOptimalTunedCommand>();
						break;
					case "12":
						command = _serviceProvider.GetService<IOptimalAddRangeCommand>();
						break;
					case "13":
						command = _serviceProvider.GetService<IAutoDetectChangesEnabledCommand>();
						break;
					case "14":
						Console.WriteLine("Execute query with UDF?");
						var withUDF = Console.ReadLine() == "y";
						var udfCommand = _serviceProvider.GetService<IUserDefinedFunctionCommand>();
						udfCommand.UseUDF = withUDF;
						command = udfCommand;
						break;
					case "15":
						command = _serviceProvider.GetService<IFromSQLProductReviewsMostValuableProductsCommand>();
						break;
					case "16":
						Console.WriteLine("Use dapper? Y/N");
						bool useDapper = Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase);
						var dapperCommand = _serviceProvider.GetService<IGetProductsWithMoreThanFiveReviewsCommand>();
						dapperCommand.UseDapper = useDapper;
						command = dapperCommand;
						break;
					case "17":
						Console.WriteLine("Use lambda in where?");
						bool useLambdaInWhere = Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase);
						var whereOrFirstCommand = _serviceProvider.GetService<ILambdasInWhereOrExecuttionCommand>();
						whereOrFirstCommand.UseLambdaInWhere= useLambdaInWhere;
						command = whereOrFirstCommand;
						break;
					case "x":
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Unknown command.");
						Console.WriteLine();
						break;
				}
				if (command == null)
				{
					continue;
				}
				var stopWatch = new Stopwatch();
				stopWatch.Start();
				Console.Clear();
				Console.WriteLine("Executing... Press CTRL + C to cancel at any time...");
				try
				{
					await command.RunAsync(_cancellationTokenSource.Token);
				}
				catch (TaskCanceledException)
				{
					Console.WriteLine("Operation canceled.");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
				finally
				{
					stopWatch.Stop();
					Console.WriteLine($"Execution time: {stopWatch.Elapsed.TotalSeconds} seconds.");
				}
			}
			Console.CancelKeyPress -= Console_Cancel;
		}

		private static void PrintIntroText()
		{
			var sb = new StringBuilder();
			sb.AppendLine()
				.AppendLine(" Options:")
				.AppendLine(" 1 -> Query list of Products (Lazy or not)")
				.AppendLine(" 2 -> Eadger Loading reviews and products")
				.AppendLine(" 3 -> Select Loading reviews and products")
				.AppendLine(" 4 -> Client vs Server Evaluation")
				.AppendLine(" 5 -> Update command")
				.AppendLine(" 6 -> Computed columns")
				.AppendLine(" 7 -> ChangeTracker.TrackGraph")
                .AppendLine(" 8 -> Not Compiled Query")
                .AppendLine(" 9 -> Compiled Query")
				.AppendLine(" 10 -> Query to improve")
				.AppendLine(" 11 -> Query already tuned")
				.AppendLine(" 12 -> AddRange")
				.AppendLine(" 13 ->  AutoDetectChangesEnabled to false on Adds")
				.AppendLine(" 14 ->  Votes average (User Defined Function or Linq query)")
				.AppendLine(" 15 ->  Votes average (using From SQL)")
				.AppendLine(" 16 ->  Get products with more than 5 reviews (dapper or FromSQL)")
				.AppendLine(" opt -> List the options")
				.AppendLine(" cls -> Clean")
				.AppendLine(" x -> Exit")
				.AppendLine();
			Console.WriteLine(sb.ToString());
		}

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IAutoDetectChangesEnabledCommand), (provider) => new AutoDetectChangesEnabledCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IClientServerWarningCommand), (provider) => new ClientServerWarningCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(ICompiledQueryCommand), (provider) => new CompiledQueryCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IComputedColumnCommand), (provider) => new ComputedColumnCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IFromSQLProductReviewsMostValuableProductsCommand), (provider) => new FromSQLProductReviewsMostValuableProductsCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IGetProductsWithMoreThanFiveReviewsCommand), (provider) => new GetProductsWithMoreThanFiveReviewsCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(INotCompiledQueryCommand), (provider) => new NotCompiledQueryCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IOptimalAddRangeCommand), (provider) => new OptimalAddRangeCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IProductReviewsLazyLoadCommand), (provider) => new ProductReviewsLazyLoadCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IQueryOptimalTunedCommand), (provider) => new QueryOptimalTunedCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IQueryToTuneCommand), (provider) => new QueryToTuneCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IReviewsProductEagerLoadCommand), (provider) => new ReviewsProductEagerLoadCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IReviewsProductSelectLoadCommand), (provider) => new ReviewsProductSelectLoadCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(ITrackGraphCommand), (provider) => new TrackGraphCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IUpdateCommand), (provider) => new UpdateCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(IUserDefinedFunctionCommand), (provider) => new UserDefinedFunctionCommand(_loggerFactory, _connectionString));
            serviceCollection.AddTransient(typeof(ILambdasInWhereOrExecuttionCommand), (provider) => new LambdasInWhereOrExecuttionCommand(_loggerFactory, _connectionString));
        }
    }
}
