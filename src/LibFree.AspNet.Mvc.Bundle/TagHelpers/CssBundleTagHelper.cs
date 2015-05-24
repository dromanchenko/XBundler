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
	public class CssBundleTagHelper : BundleTagHelper
	{
		[Activate]
		[HtmlAttributeNotBound]
		public ICssMinifier CssMinifier { get; set; }

		protected override ILogger GetLogger()
		{
			return LoggerFactory.CreateLogger<CssBundleTagHelper>();
		}

		protected override string GetLoggerMessagesPrefix()
		{
			return "CssBundleTagHelper";
		}

		internal override async Task<Bundle> CreateBundle(TagHelperContext context)
		{
			var cssTagsContent = await context.GetChildContentAsync();
			var filePaths = HtmlParser.ParseCssBundle(cssTagsContent.GetContent());
			return new CssBundle(VirtualPath, filePaths, CssMinifier, HostingEnvironment);
		}

		internal override void SetContent(TagHelperOutput output, Bundle bundle)
		{
			output.Content.SetContent(string.Format("<link href='{0}' rel='stylesheet' type='text/css'/>", bundle.GeneratedVirtualPath));
		}
	}
}