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
			_logger.LogVerbose("BundleMiddleware: running");

			var requestPath = context.Request.Path.Value;
			var requestPathWithoutQueryString = requestPath.Contains("?")
				? requestPath.Remove(requestPath.IndexOf('?'))
				: requestPath;
			if (BundleRuntime.Bundles.ContainsKey(requestPathWithoutQueryString))
			{
				_logger.LogVerbose("BundleMiddleware: request path {0} matches one of the bundles", requestPath);

				var bundle = BundleRuntime.Bundles[requestPathWithoutQueryString];
				if (bundle is CssBundle)
				{
					context.Response.ContentType = "text/css";
					context.Response.Headers["Cache-Control"] = "private, max-age=86400, stale-while-revalidate=604800";
                    var content = await bundle.GetContent();
					await context.Response.WriteAsync(content);
				}
				else
				{
					var ex = new NotImplementedException(string.Format("Bundle type {0} is not implemented", bundle.GetType().FullName));
					_logger.LogError(string.Format("BundleMiddleware: bundle type {0} is not implemented", bundle.GetType().FullName), ex);
					throw ex;
				}
			}
			else
			{
				await _next(context);
			}
		}
	}
}