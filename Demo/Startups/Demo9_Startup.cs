using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Startups
{
	public class Demo9_Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// configuration of the DI
		}

		public void Configure(IApplicationBuilder app)
		{
			//app.UseMiddleware<MyHttpRequestIdentifierFeatureMiddleware>();
			app.Run(async context => { await context.Response.WriteAsync("Demo"); });
		}
	}
}