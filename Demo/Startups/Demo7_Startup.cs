using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Startups
{
	public class Demo7_Startup
	{
		private const string _SCOPE_NAME = "ASPNET_WEBSERVER_ROOT";

		private readonly ILifetimeScope _lifetimeScope;
		private ILifetimeScope _aspnetScope;

		public Demo7_Startup(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// configuration of the DI

			_aspnetScope = _lifetimeScope.BeginLifetimeScope(_SCOPE_NAME, builder => builder.Populate(services, _SCOPE_NAME));

			return new AutofacServiceProvider(_aspnetScope);
		}

		public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime)
		{
			app.Run(async context => { await context.Response.WriteAsync("Demo"); });

			applicationLifetime.ApplicationStopped.Register(() => _aspnetScope?.Dispose());
		}
	}
}