using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Startups
{
	public class Demo6_Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// configuration of the DI
		}

		public void Configure(IApplicationBuilder app)
		{
			app.Run(async context => { await context.Response.WriteAsync("Demo"); });
		}
	}
}