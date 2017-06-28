using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Demo.Middlewares
{
	public class MyHttpRequestIdentifierFeatureMiddleware
	{
		private readonly RequestDelegate _next;

		public MyHttpRequestIdentifierFeatureMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext ctx)
		{
			ctx.Features.Set<IHttpRequestIdentifierFeature>(new MyHttpRequestIdentifierFeature());

			await _next(ctx);
		}
	}

	public class MyHttpRequestIdentifierFeature : IHttpRequestIdentifierFeature
	{
		public string TraceIdentifier { get; set; } = Guid.NewGuid().ToString();
	}
}