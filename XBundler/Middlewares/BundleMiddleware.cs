﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using XBundler.Core.Abstractions;
using XBundler.Core.Bundles;
using System;
using System.Threading.Tasks;

namespace XBundler.Core.Middlewares
{
	public sealed class BundleMiddleware
    {
		private readonly RequestDelegate _next;
		private ILogger _logger;
		private IBundleRuntime _bundleRuntime;

		public BundleMiddleware(RequestDelegate next,
			ILoggerFactory loggerFactory,
			IBundleRuntime bundleRuntime)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<BundleMiddleware>();
			_bundleRuntime = bundleRuntime;
        }

		public async Task Invoke(HttpContext context)
		{
			_logger.LogDebug("BundleMiddleware: running");

			var requestPath = context.Request.Path.Value;
			var requestPathWithoutQueryString = requestPath.Contains("?")
				? requestPath.Remove(requestPath.IndexOf('?'))
				: requestPath;
			var bundle = _bundleRuntime.GetBundle(requestPathWithoutQueryString);
			if (bundle != null)
			{
				_logger.LogDebug("BundleMiddleware: request path {0} matches one of the bundles", requestPath);

				string content;
				if (bundle is CssBundle)
				{
					context.Response.ContentType = "text/css; charset=utf-8";
				}
				else if (bundle is JsBundle)
				{
					context.Response.ContentType = "application/javascript; charset=utf-8";
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