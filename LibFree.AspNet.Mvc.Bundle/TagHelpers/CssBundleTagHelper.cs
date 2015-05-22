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
			/*var htmlDocument = new HtmlAgilityPack.HtmlDocument();
			htmlDocument.LoadHtml(cssTagsContent.GetContent());
			var cssNodes = htmlDocument.DocumentNode.SelectNodes("/css");
			foreach (var cssNode in cssNodes)
			{
				var href = cssNode.Attributes["href"];
				int a = 0;
			}*/
		}

		/*private void ReadFileName()
		{
			if (_cachedFileName == null)
			{
				lock(_cacheFileNameLockObject)
				{
					if (_cachedFileName == null)
					{
						var revManifest = File.ReadAllText(Path.Combine(HostingEnvironment.WebRootPath, "..\\rev-manifest.json"));
						var htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(revManifest);
					}
				}
			}
		}*/
	}
}