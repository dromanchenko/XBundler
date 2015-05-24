using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace LibFree.AspNet.Mvc.Bundle.TagHelpers
{
	public abstract class BundleTagHelper : TagHelper
	{
		[Activate]
		[HtmlAttributeNotBound]

		public IHostingEnvironment HostingEnvironment { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public ILoggerFactory LoggerFactory { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public IHtmlParser HtmlParser { get; set; }

		[HtmlAttributeName("virtualPath")]
		public string VirtualPath { get; set; }

		protected abstract ILogger GetLogger();
		protected abstract string GetLoggerMessagesPrefix();
		internal virtual async Task<Bundle> CreateBundle(TagHelperContext context)
		{
			return await Task.FromResult<Bundle>(null);
		}

		internal abstract void SetContent(TagHelperOutput output, Bundle bundle);

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var logger = GetLogger();
			var loggerMessagesPrefix = GetLoggerMessagesPrefix();
			logger.LogVerbose("{0}: running", loggerMessagesPrefix);

			output.SuppressOutput();

			Bundle bundle;
			if (BundleRuntime.Bundles.ContainsKey(VirtualPath))
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
			}

			SetContent(output, bundle);
		}
	}
}