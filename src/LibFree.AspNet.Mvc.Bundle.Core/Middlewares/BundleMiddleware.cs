using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using LibFree.AspNet.Mvc.Bundle.Core.Bundles;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.Middlewares
{
	public sealed class BundleMiddleware
    {
		private readonly RequestDelegate _next;
		private ILogger _logger;
		private IBundleRuntime _bundleRuntime;

		public BundleMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<BundleMiddleware>();
			_bundleRuntime = bundleRuntime;
		}

		public async Task Invoke(HttpContext context)
		{
			_logger.LogVerbose("BundleMiddleware: running");

			var requestPath = context.Request.Path.Value;
			var requestPathWithoutQueryString = requestPath.Contains("?")
				? requestPath.Remove(requestPath.IndexOf('?'))
				: requestPath;
			if (_bundleRuntime.Bundles.ContainsKey(requestPathWithoutQueryString))
			{
				_logger.LogVerbose("BundleMiddleware: request path {0} matches one of the bundles", requestPath);

				var bundle = _bundleRuntime.Bundles[requestPathWithoutQueryString];
				string content;
				if (bundle is CssBundle)
				{
					context.Response.ContentType = "text/css";
				}
				else if (bundle is JsBundle)
				{
					context.Response.ContentType = "application/x-javascript";
				}
				else
				{
					var ex = new NotImplementedException(string.Format("Bundle type {0} is not implemented", bundle.GetType().FullName));
					_logger.LogError(string.Format("BundleMiddleware: bundle type {0} is not implemented", bundle.GetType().FullName), ex);
					throw ex;
				}

				context.Response.Headers["Cache-Control"] = "public, max-age=31536000, stale-while-revalidate=86400, stale-if-error=259200";
				content = await bundle.GetContent();
				await context.Response.WriteAsync(content);
			}
			else
			{
				await _next(context);
			}
		}
	}
}