using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using LibFree.AspNet.Mvc.Bundle.Services;
using Microsoft.Framework.Logging;
using System;

namespace LibFree.AspNet.Mvc.Bundle.TagHelpers
{
	[TargetElement("cssbundle")]
	public class CssBundleTagHelper : TagHelper
    {
		[Activate]
		[HtmlAttributeNotBound]
		public IHostingEnvironment HostingEnvironment { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public ILoggerFactory LoggerFactory { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public ICssMinifier CssMinifier { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public IHtmlParser HtmlParser { get; set; }

		[HtmlAttributeName("virtualPath")]
		public string VirtualPath { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var logger = LoggerFactory.CreateLogger<CssBundleTagHelper>();
			logger.LogVerbose("CssBundleTagHelper: running");

			output.SuppressOutput();

			Bundle bundle;
			if (BundleRuntime.Bundles.ContainsKey(VirtualPath))
			{
				logger.LogVerbose("CssBundleTagHelper: 1st round check - bundle {0} already exists. Using it", VirtualPath);
				bundle = BundleRuntime.Bundles[VirtualPath];
			}
			else
			{
				
				await BundleRuntime.BundlesSyncLock.WaitAsync();
				if (BundleRuntime.Bundles.ContainsKey(VirtualPath))
				{
					logger.LogVerbose("CssBundleTagHelper: 2nd round check - bundle {0} already exists. Using it", VirtualPath);
					bundle = BundleRuntime.Bundles[VirtualPath];
				}
				else
				{
					try
					{
						logger.LogVerbose("CssBundleTagHelper: bundle {0} doesn't exist. Creating it", VirtualPath);

						var cssTagsContent = await context.GetChildContentAsync();
						var filePaths = HtmlParser.ParseCssBundle(cssTagsContent.GetContent());
						bundle = new CssBundle(VirtualPath, filePaths, CssMinifier, HostingEnvironment);
						BundleRuntime.Bundles.Add(VirtualPath, bundle);
					}
					catch (Exception ex)
					{
						logger.LogError(string.Format("CssBundleTagHelper: exception is thrown while creating bundle {0}", VirtualPath),
							ex);
						throw;
					}
					finally
					{
						BundleRuntime.BundlesSyncLock.Release();
					}
				}
			}
			
			output.Content.SetContent(string.Format("<link href='{0}' rel='stylesheet' type='text/css'/>", bundle.GeneratedVirtualPath));
		}
	}
}