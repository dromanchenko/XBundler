using LibFree.AspNet.Mvc.Bundle.Core.Abstractions;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.Core.TagHelpers
{
	public abstract class BundleTagHelper : TagHelper
	{
		private ILoggerFactory _loggerFactory;
		private IBundleRuntime _bundleRuntime;

		[HtmlAttributeName("virtualPath")]
		public string VirtualPath { get; set; }

		public BundleTagHelper(ILoggerFactory loggerFactory, IBundleRuntime bundleRuntime)
		{
			_loggerFactory = loggerFactory;
			_bundleRuntime = bundleRuntime;
        }

		protected abstract ILogger GetLogger(ILoggerFactory loggerFactory);
		protected abstract string GetLoggerMessagesPrefix();
		protected abstract IEnumerable<string> ParseHtml(string content);
		protected abstract void SetContent(TagHelperOutput output, Bundles.Bundle bundle);
		protected abstract BundleType GetBundleType();

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var logger = GetLogger(_loggerFactory);
			var loggerMessagesPrefix = GetLoggerMessagesPrefix();
			logger.LogVerbose("{0}: running", loggerMessagesPrefix);

			output.SuppressOutput();

			var bundleType = GetBundleType();
			var childContent = await context.GetChildContentAsync();
			var bundle = _bundleRuntime.GetOrCreateBundle(bundleType, VirtualPath, ParseHtml(childContent.GetContent()), loggerMessagesPrefix);
			/*if (BundleRuntime.Bundles.ContainsKey(VirtualPath))
			{
				logger.LogVerbose("{0}: 1st round check - bundle {1} already exists. Using it", loggerMessagesPrefix, VirtualPath);
				bundle = BundleRuntime.Bundles[VirtualPath];
			}
			else
			{
				await BundleRuntime.BundlesSyncLock.WaitAsync();
				if (BundleRuntime.Bundles.ContainsKey(VirtualPath))
				{
					logger.LogVerbose("{0}: 2nd round check - bundle {1} already exists. Using it", loggerMessagesPrefix, VirtualPath);
					bundle = BundleRuntime.Bundles[VirtualPath];
				}
				else
				{
					try
					{
						logger.LogVerbose("{0}: bundle {1} doesn't exist. Creating it", loggerMessagesPrefix, VirtualPath);
						bundle = await CreateBundle(context);
						BundleRuntime.Bundles.Add(VirtualPath, bundle);
					}
					catch (Exception ex)
					{
						logger.LogError(string.Format("{0}: exception is thrown while creating bundle {1}", loggerMessagesPrefix, VirtualPath),
							ex);
						throw;
					}
					finally
					{
						BundleRuntime.BundlesSyncLock.Release();
					}
				}
			}*/

			SetContent(output, bundle);
		}
	}
}