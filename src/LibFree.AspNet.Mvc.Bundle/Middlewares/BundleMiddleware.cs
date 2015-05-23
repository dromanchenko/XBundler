using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Middlewares
{
    public sealed class BundleMiddleware
    {
		private readonly RequestDelegate _next;
		private ILogger _logger;

		public BundleMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<BundleMiddleware>();
		}

		public async Task Invoke(HttpContext context)
		{
			_logger.LogVerbose("RequestDelegate: running");

			var a = context.Request.PathBase;

			await _next(context);
		}
	}
}