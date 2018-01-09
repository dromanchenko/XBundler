using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using XBundler.Core.Abstractions;
using XBundler.Core.Bundles;

namespace XBundler.Core
{
	internal sealed class BundleRuntime : IBundleRuntime
    {
		private readonly ConcurrentDictionary<string, Bundle> _bundles = new ConcurrentDictionary<string, Bundle>();
		private ILogger _logger;
		private IHostingEnvironment _hostingEnvironment;
		private ICssMinifier _cssMinifier;
		private IJsMinifier _jsMinifier;

		public BundleRuntime(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment, ICssMinifier cssMinifier, IJsMinifier jsMinifier)
		{
			_logger = loggerFactory.CreateLogger<BundleRuntime>();
			_hostingEnvironment = hostingEnvironment;
			_cssMinifier = cssMinifier;
			_jsMinifier = jsMinifier;
        }

		public Bundle GetBundle(string virtualPath)
		{
			Bundle bundle;
			_bundles.TryGetValue(virtualPath, out bundle);
			return bundle;
		}

		public Bundle CreateBundle(BundleType bundleType, string virtualPath, string targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix)
		{
			var targetEnvironmentsParsed = targetEnvironments != null ? targetEnvironments.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
			return CreateBundle(bundleType, virtualPath, targetEnvironmentsParsed, filePaths, loggerMessagesPrefix);
		}

		public Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix)
		{
			var bundle = _bundles.GetOrAdd(virtualPath, (v) =>
			{
				try
				{
					_logger.LogDebug("{0}: bundle {1} doesn't exist. Creating it", loggerMessagesPrefix, v);
					return CreateBundle(bundleType, v, targetEnvironments, filePaths);
				}
				catch (Exception ex)
				{
					_logger.LogError(string.Format("{0}: exception is thrown while creating bundle {1}", loggerMessagesPrefix, virtualPath),
						ex);
					throw;
				}
			});

			return bundle;
		}

		private Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths)
		{
			Bundle bundle = null;

			switch (bundleType)
			{
				case BundleType.Css:
					bundle = new CssBundle(virtualPath, filePaths, _cssMinifier, _hostingEnvironment, targetEnvironments);
					break;
				case BundleType.Js:
					bundle = new JsBundle(virtualPath, filePaths, _jsMinifier, _hostingEnvironment, targetEnvironments);
					break;
				default:
					throw new NotImplementedException();
			}

			return bundle;
		}
	}
}