using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using LibFree.AspNet.Mvc.Bundle.Services;

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
		public ICssMinifier CssMinifier { get; set; }

		[Activate]
		[HtmlAttributeNotBound]
		public IHtmlParser HtmlParser { get; set; }

		[HtmlAttributeName("virtualPath")]
		public string VirtualPath { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.SuppressOutput();
			var cssTagsContent = await context.GetChildContentAsync();
			var filePaths = HtmlParser.ParseCssBundle(cssTagsContent.GetContent());
		}
	}
}