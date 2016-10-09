using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using LibFree.AspNet.Mvc.Bundle.Core.Bundles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Hosting;

namespace LibFree.AspNet.Mvc.Bundle.Core
{
	internal sealed class BundleRuntime : IBundleRuntime
    {
		private readonly Dictionary<string, Bundles.Bundle> _bundles;
		private readonly ReadOnlyDictionary<string, Bundles.Bundle> _bundleReadOnly;
        public ReadOnlyDictionary<string, Bundles.Bundle> Bundles
		{
			get
			{
				return _bundleReadOnly;
			}
		}

		private ILogger _logger;
		private IHostingEnvironment _hostingEnvironment;
		private ICssMinifier _cssMinifier;
		private IJsMinifier _jsMinifier;

		public BundleRuntime(ILoggerFactory loggerFactory, IHostingEnvironment hostingEnvironment, ICssMinifier cssMinifier, IJsMinifier jsMinifier)
		{
			_bundles = new Dictionary<string, Bundles.Bundle>();
			_bundleReadOnly = new ReadOnlyDictionary<string, Bundles.Bundle>(_bundles);
			_logger = loggerFactory.CreateLogger<BundleRuntime>();
			_hostingEnvironment = hostingEnvironment;
			_cssMinifier = cssMinifier;
			_jsMinifier = jsMinifier;
        }

		public Bundles.Bundle CreateBundle(BundleType bundleType, string virtualPath, string targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix)
		{
			var targetEnvironmentsParsed = targetEnvironments != null ? targetEnvironments.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
			return CreateBundle(bundleType, virtualPath, targetEnvironmentsParsed, filePaths, loggerMessagesPrefix);
		}

		public Bundles.Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths, string loggerMessagesPrefix)
		{
			Bundles.Bundle bundle;
			if (_bundles.ContainsKey(virtualPath))
			{
				_logger.LogDebug("{0}: 1st round check - bundle {1} already exists. Using it", loggerMessagesPrefix, virtualPath);
				bundle = _bundles[virtualPath];
			}
			else
			{
				lock (_bundles)
				{
					if (_bundles.ContainsKey(virtualPath))
					{
						_logger.LogDebug("{0}: 2nd round check - bundle {1} already exists. Using it", loggerMessagesPrefix, virtualPath);
						bundle = _bundles[virtualPath];
					}
					else
					{
						try
						{
							_logger.LogDebug("{0}: bundle {1} doesn't exist. Creating it", loggerMessagesPrefix, virtualPath);
							bundle = CreateBundle(bundleType, virtualPath, targetEnvironments, filePaths);
							_bundles.Add(virtualPath, bundle);
						}
						catch (Exception ex)
						{
							_logger.LogError(string.Format("{0}: exception is thrown while creating bundle {1}", loggerMessagesPrefix, virtualPath),
								ex);
							throw;
						}
					}
				}
			}

			return bundle;
		}

		private Bundles.Bundle CreateBundle(BundleType bundleType, string virtualPath, IEnumerable<string> targetEnvironments, IEnumerable<string> filePaths)
		{
			Bundles.Bundle bundle = null;

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