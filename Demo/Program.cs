using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using Autofac;
using Demo.Configurations;
using Demo.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Serilog;

namespace Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			Demo1_DependencyInjection();
			//Demo2_Configuration();
			//Demo3_Configuration_StronglyTyped();
			//Demo4_Logging();
			//Demo5_Kestrel();
			//Demo6_Startup();
			//Demo7_Startup_Controlled_DI();
			//Demo8_Startup_MVC();
			//Demo9_Middleware();

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private static void Demo1_DependencyInjection()
		{
			IServiceCollection serviceCollection = new ServiceCollection()
				.AddTransient<HttpClient>();

			var serviceProvider = serviceCollection.BuildServiceProvider();

			using (var scope = serviceProvider.CreateScope())
			{
				var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
				Console.WriteLine($"Resoved type: {httpClient.GetType().Name}");
			}
		}

		private static void Demo2_Configuration()
		{
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
				.AddInMemoryCollection(new Dictionary<string, string>() {{"Key", "Value"}});

			IConfiguration configuration = configurationBuilder.Build();

			var value = configuration["Key"];
			Console.WriteLine($"Value = {value}");
		}

		private static void Demo3_Configuration_StronglyTyped()
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.AddJsonFile("Configuration.json")
				.Build();

			IServiceProvider serviceProvider = new ServiceCollection()
				.AddOptions()
				.Configure<MyConfiguration>(configuration.GetSection("my:configuration"))
				.BuildServiceProvider();

			// Won't work
			// MyConfiguration myConfiguration = serviceProvider.GetRequiredService<MyConfiguration>(); 

			IOptions<MyConfiguration> optionsOfMyConfiguration = serviceProvider.GetRequiredService<IOptions<MyConfiguration>>();
			MyConfiguration myConfiguration = optionsOfMyConfiguration.Value;

			Console.WriteLine($"Value = {myConfiguration.Value}");
		}

		private static void Demo4_Logging()
		{
			ILoggerFactory loggerFactory = new LoggerFactory()
				.AddConsole(includeScopes: true);

			ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

			using (logger.BeginScope("Demo4 Scope"))
			{
				logger.LogInformation(42, "Demo at {date}", DateTime.Now);
			}
		}

		private static void Demo5_Kestrel()
		{
			IWebHost host = new WebHostBuilder()
				.UseKestrel()
				.Configure(app => { app.Run(async context => await context.Response.WriteAsync("Demo")); })
				.Build();

			host.Run();
		}

		private static void Demo6_Startup()
		{
			IWebHost host = new WebHostBuilder()
				.UseKestrel()
				.UseStartup<Demo6_Startup>()
				.Build();

			host.Run();
		}

		private static void Demo7_Startup_Controlled_DI()
		{
			var containerBuilder = new ContainerBuilder();
			// configure container

			using (var container = containerBuilder.Build())
			{
				// do something

				using (var scope = container.BeginLifetimeScope(builder => builder.RegisterType<Demo7_Startup>().AsSelf()))
				{
					IWebHost host = new WebHostBuilder()
						.UseKestrel()
						.UseStartup<Demo7_Startup>()
						.ConfigureServices(services => { services.AddTransient(provider => scope.Resolve<Demo7_Startup>()); })
						.Build();

					host.Run();
				}
			}
		}

		private static void Demo8_Startup_MVC()
		{
			IWebHost host = new WebHostBuilder()
				.UseKestrel()
				.UseStartup<Demo8_Startup>()
				.Build();

			host.Run();
		}

		private static void Demo9_Middleware()
		{
			var serilogLogger = new LoggerConfiguration()
				.WriteTo.LiterateConsole(outputTemplate: "[{Level:u3} from {SourceContext}] [Request: {RequestId}] {Message}{NewLine}{Exception}")
				.CreateLogger();
			var loggerFactory = new LoggerFactory()
				.AddSerilog(serilogLogger);

			IWebHost host = new WebHostBuilder()
				.UseKestrel()
				.UseLoggerFactory(loggerFactory)
				.UseStartup<Demo9_Startup>()
				.Build();

			host.Run();
		}
	}
}